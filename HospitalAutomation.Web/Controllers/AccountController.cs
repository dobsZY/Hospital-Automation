using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Utilities;
using HospitalAutomation.Web.ViewModels;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;

        public AccountController(IUserService userService, IPatientService patientService)
        {
            _userService = userService;
            _patientService = patientService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // First try to authenticate as staff/user
                var user = _userService.Authenticate(model.Username, model.Password);
                if (user != null)
                {
                    var claimsPrincipal = SessionManager.CreateClaimsPrincipal(user);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

                    // Redirect based on role
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", GetHomeActionByRole(user.Role));
                }

                // If not staff, try patient authentication
                var patient = _patientService.GetPatientByNationalId(model.Username);
                if (patient != null)
                {
                    var expectedPassword = patient.BirthDate.ToString("ddMM");
                    if (model.Password == expectedPassword || model.Password == patient.NationalId)
                    {
                        var claimsPrincipal = SessionManager.CreateClaimsPrincipal(patient);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index", "PatientPortal");
                    }
                }

                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Giriş sırasında hata oluştu: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Check if NationalId already exists
                if (_patientService.IsNationalIdExists(model.NationalId))
                {
                    ModelState.AddModelError("", "Bu TC Kimlik No ile kayıtlı hasta bulunmaktadır");
                    return View(model);
                }

                var patient = new Patient
                {
                    NationalId = model.NationalId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                    Phone = model.Phone,
                    Email = model.Email,
                    Address = model.Address,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                if (_patientService.CreatePatient(patient))
                {
                    TempData["SuccessMessage"] = "Kayıt başarıyla tamamlandı! Giriş yapabilirsiniz.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "Kayıt işlemi başarısız oldu");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Kayıt sırasında hata oluştu: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login");
                }

                if (_userService.ChangePassword(currentUser.Id, model.OldPassword, model.NewPassword))
                {
                    TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirildi";
                    return RedirectToAction("Profile");
                }

                ModelState.AddModelError("", "Mevcut şifre hatalı veya şifre değiştirme işlemi başarısız oldu");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Şifre değiştirme sırasında hata oluştu: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            var currentUser = SessionManager.GetCurrentUser(HttpContext);
            if (currentUser != null)
            {
                var user = _userService.GetUserById(currentUser.Id);
                return View(user);
            }

            var currentPatient = SessionManager.GetCurrentPatient(HttpContext);
            if (currentPatient != null)
            {
                var patient = _patientService.GetPatientById(currentPatient.Id);
                return View("PatientProfile", patient);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private string GetHomeActionByRole(UserRole role)
        {
            return role switch
            {
                UserRole.Doctor => "Doctor",
                UserRole.Nurse => "Nurse",
                UserRole.Admin => "Home",
                UserRole.Receptionist => "Home",
                _ => "Home"
            };
        }
    }
}


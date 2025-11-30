using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;
using HospitalAutomation.Data.Interfaces;

namespace HospitalAutomation.Web.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(UserRole? roleFilter = null)
        {
            try
            {
                var users = roleFilter.HasValue
                    ? _userService.GetUsersByRole(roleFilter.Value)
                    : _userService.GetAllUsers();

                ViewBag.RoleFilter = roleFilter;
                return View(users.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Kullanıcılar yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<User>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadViewBagData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user, string password)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadViewBagData();
                    return View(user);
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    ModelState.AddModelError("", "Şifre gereklidir");
                    LoadViewBagData();
                    return View(user);
                }

                if (_userService.CreateUser(user, password))
                {
                    TempData["SuccessMessage"] = "Kullanıcı başarıyla oluşturuldu!";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Kullanıcı oluşturulamadı");
                LoadViewBagData();
                return View(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                LoadViewBagData();
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Kullanıcı bulunamadı";
                    return RedirectToAction("Index");
                }

                LoadViewBagData();
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadViewBagData();
                    return View(user);
                }

                if (_userService.UpdateUser(user))
                {
                    TempData["SuccessMessage"] = "Kullanıcı başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Kullanıcı güncellenemedi");
                LoadViewBagData();
                return View(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                LoadViewBagData();
                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_userService.DeleteUser(id))
                {
                    TempData["SuccessMessage"] = "Kullanıcı başarıyla silindi!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Kullanıcı silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Kullanıcı bulunamadı";
                    return RedirectToAction("Index");
                }

                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        private void LoadViewBagData()
        {
            ViewBag.Departments = _unitOfWork.Departments.GetActiveDepartments().ToList();
            ViewBag.Roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().ToList();
        }
    }
}


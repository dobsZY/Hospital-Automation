using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Web.Controllers
{
    [Authorize(Policy = "DoctorOnly")]
    public class DoctorController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorController(
            IAppointmentService appointmentService,
            IPatientService patientService,
            IUnitOfWork unitOfWork)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                ViewBag.UserName = SessionManager.GetDisplayName(HttpContext);
                
                // Get today's appointments
                var todayAppointments = _appointmentService.GetAppointmentsByDoctor(currentUser.Id)
                    .Where(a => a.AppointmentDate.Date == DateTime.Today.Date)
                    .ToList();
                
                ViewBag.TodayAppointmentsCount = todayAppointments.Count;
                ViewBag.TodayAppointments = todayAppointments;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Dashboard yüklenirken hata oluştu: {ex.Message}";
                return View();
            }
        }

        public IActionResult Appointments(DateTime? date = null)
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var selectedDate = date ?? DateTime.Today;
                var appointments = _appointmentService.GetAppointmentsByDoctor(currentUser.Id)
                    .Where(a => a.AppointmentDate.Date == selectedDate.Date)
                    .ToList();

                ViewBag.SelectedDate = selectedDate;
                return View(appointments);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Randevular yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<Appointment>());
            }
        }

        public IActionResult Patients()
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Get patients who have appointments with this doctor
                var appointments = _appointmentService.GetAppointmentsByDoctor(currentUser.Id);
                var patientIds = appointments.Select(a => a.PatientId).Distinct();
                var patients = _patientService.GetAllPatients()
                    .Where(p => patientIds.Contains(p.Id))
                    .ToList();

                return View(patients);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hastalar yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<Patient>());
            }
        }

        public IActionResult MedicalRecords(int? patientId = null)
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var records = patientId.HasValue
                    ? _unitOfWork.MedicalRecords.GetRecordsByPatient(patientId.Value)
                    : _unitOfWork.MedicalRecords.GetRecordsByDoctor(currentUser.Id);

                ViewBag.PatientId = patientId;
                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                return View(records.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Tıbbi kayıtlar yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<MedicalRecord>());
            }
        }

        [HttpGet]
        public IActionResult CreateMedicalRecord(int? appointmentId = null, int? patientId = null)
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var record = new MedicalRecord
                {
                    DoctorId = currentUser.Id,
                    AppointmentId = appointmentId,
                    PatientId = patientId ?? 0,
                    RecordDate = DateTime.Now
                };

                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                ViewBag.Appointments = appointmentId.HasValue
                    ? _appointmentService.GetAppointmentsByDoctor(currentUser.Id).ToList()
                    : Enumerable.Empty<Appointment>().ToList();

                return View(record);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("MedicalRecords");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMedicalRecord(MedicalRecord record)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var currentUser = SessionManager.GetCurrentUser(HttpContext);
                    ViewBag.Patients = _patientService.GetAllPatients().ToList();
                    ViewBag.Appointments = _appointmentService.GetAppointmentsByDoctor(currentUser.Id).ToList();
                    return View(record);
                }

                record.RecordDate = DateTime.Now;
                record.CreatedDate = DateTime.Now;
                record.IsActive = true;

                _unitOfWork.MedicalRecords.Add(record);
                _unitOfWork.Complete();

                TempData["SuccessMessage"] = "Tıbbi kayıt başarıyla oluşturuldu!";
                return RedirectToAction("MedicalRecords");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                ViewBag.Appointments = _appointmentService.GetAppointmentsByDoctor(currentUser.Id).ToList();
                return View(record);
            }
        }

        [HttpGet]
        public IActionResult Profile()
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = _unitOfWork.Users.GetById(currentUser.Id);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Kullanıcı bulunamadı";
                    return RedirectToAction("Index");
                }

                ViewBag.Departments = _unitOfWork.Departments.GetActiveDepartments().ToList();
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
        public IActionResult Profile(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Departments = _unitOfWork.Departments.GetActiveDepartments().ToList();
                    return View(user);
                }

                // Use UserService to update
                var userService = HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;
                if (userService != null && userService.UpdateUser(user))
                {
                    TempData["SuccessMessage"] = "Profil başarıyla güncellendi!";
                    return RedirectToAction("Profile");
                }

                ModelState.AddModelError("", "Profil güncellenemedi");
                ViewBag.Departments = _unitOfWork.Departments.GetActiveDepartments().ToList();
                return View(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                ViewBag.Departments = _unitOfWork.Departments.GetActiveDepartments().ToList();
                return View(user);
            }
        }
    }
}


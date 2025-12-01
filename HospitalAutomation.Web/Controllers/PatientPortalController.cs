using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Web.Controllers
{
    [Authorize]
    public class PatientPortalController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public PatientPortalController(
            IAppointmentService appointmentService,
            IPatientService patientService,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public IActionResult Index()
        {
            try
            {
                var currentPatient = SessionManager.GetCurrentPatient(HttpContext);
                if (currentPatient == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                ViewBag.PatientName = SessionManager.GetDisplayName();
                
                // Get upcoming appointments
                var upcomingAppointments = _appointmentService.GetAppointmentsByPatient(currentPatient.Id)
                    .Where(a => a.AppointmentDate >= DateTime.Today && a.Status == AppointmentStatus.Scheduled)
                    .OrderBy(a => a.AppointmentDate)
                    .ThenBy(a => a.AppointmentTime)
                    .Take(5)
                    .ToList();
                
                ViewBag.UpcomingAppointments = upcomingAppointments;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Dashboard yüklenirken hata oluştu: {ex.Message}";
                return View();
            }
        }

        public IActionResult Appointments()
        {
            try
            {
                var currentPatient = SessionManager.GetCurrentPatient(HttpContext);
                if (currentPatient == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var appointments = _appointmentService.GetAppointmentsByPatient(currentPatient.Id)
                    .OrderByDescending(a => a.AppointmentDate)
                    .ThenByDescending(a => a.AppointmentTime)
                    .ToList();

                return View(appointments);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Randevular yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<Appointment>());
            }
        }

        [HttpGet]
        public IActionResult CreateAppointment()
        {
            try
            {
                var currentPatient = SessionManager.GetCurrentPatient(HttpContext);
                if (currentPatient == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var appointment = new Appointment
                {
                    PatientId = currentPatient.Id,
                    AppointmentDate = DateTime.Today,
                    Status = AppointmentStatus.Scheduled
                };

                ViewBag.Doctors = _userService.GetDoctors().ToList();
                ViewBag.Departments = _unitOfWork.Departments.GetActiveDepartments().ToList();
                return View(appointment);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("Appointments");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAppointment(Appointment appointment)
        {
            try
            {
                var currentPatient = SessionManager.GetCurrentPatient(HttpContext);
                if (currentPatient == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                appointment.PatientId = currentPatient.Id;

                if (!ModelState.IsValid)
                {
                    ViewBag.Doctors = _userService.GetDoctors().ToList();
                    ViewBag.Departments = _unitOfWork.Departments.GetActiveDepartments().ToList();
                    return View(appointment);
                }

                // Check time slot availability
                if (!_appointmentService.IsTimeSlotAvailable(appointment.DoctorId, appointment.AppointmentDate, appointment.AppointmentTime))
                {
                    ModelState.AddModelError("", "Bu zaman dilimi müsait değil!");
                    ViewBag.Doctors = _userService.GetDoctors().ToList();
                    ViewBag.Departments = _unitOfWork.Departments.GetActiveDepartments().ToList();
                    return View(appointment);
                }

                if (_appointmentService.CreateAppointment(appointment))
                {
                    TempData["SuccessMessage"] = "Randevu başarıyla oluşturuldu!";
                    return RedirectToAction("Appointments");
                }

                ModelState.AddModelError("", "Randevu oluşturulamadı");
                ViewBag.Doctors = _userService.GetDoctors().ToList();
                ViewBag.Departments = _unitOfWork.Departments.GetActiveDepartments().ToList();
                return View(appointment);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                ViewBag.Doctors = _userService.GetDoctors().ToList();
                ViewBag.Departments = _unitOfWork.Departments.GetActiveDepartments().ToList();
                return View(appointment);
            }
        }

        public IActionResult MedicalRecords()
        {
            try
            {
                var currentPatient = SessionManager.GetCurrentPatient(HttpContext);
                if (currentPatient == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var records = _unitOfWork.MedicalRecords.GetRecordsByPatient(currentPatient.Id)
                    .OrderByDescending(r => r.RecordDate)
                    .ToList();

                return View(records);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Tıbbi kayıtlar yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<MedicalRecord>());
            }
        }

        [HttpGet]
        public IActionResult Profile()
        {
            try
            {
                var currentPatient = SessionManager.GetCurrentPatient(HttpContext);
                if (currentPatient == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var patient = _patientService.GetPatientById(currentPatient.Id);
                if (patient == null)
                {
                    TempData["ErrorMessage"] = "Hasta bulunamadı";
                    return RedirectToAction("Index");
                }

                ViewBag.Cities = _unitOfWork.Cities.GetAll().ToList();
                ViewBag.Districts = _unitOfWork.Districts.GetAll().ToList();
                return View(patient);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(Patient patient)
        {
            try
            {
                var currentPatient = SessionManager.GetCurrentPatient(HttpContext);
                if (currentPatient == null || currentPatient.Id != patient.Id)
                {
                    TempData["ErrorMessage"] = "Yetkisiz erişim";
                    return RedirectToAction("Index");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Cities = _unitOfWork.Cities.GetAll().ToList();
                    ViewBag.Districts = _unitOfWork.Districts.GetAll().ToList();
                    return View(patient);
                }

                if (_patientService.UpdatePatient(patient))
                {
                    TempData["SuccessMessage"] = "Profil başarıyla güncellendi!";
                    return RedirectToAction("Profile");
                }

                ModelState.AddModelError("", "Profil güncellenemedi");
                ViewBag.Cities = _unitOfWork.Cities.GetAll().ToList();
                ViewBag.Districts = _unitOfWork.Districts.GetAll().ToList();
                return View(patient);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                ViewBag.Cities = _unitOfWork.Cities.GetAll().ToList();
                ViewBag.Districts = _unitOfWork.Districts.GetAll().ToList();
                return View(patient);
            }
        }
        [HttpGet]
        public IActionResult GetAvailableTimeSlots(int doctorId, DateTime date)
        {
            try
            {
                var timeSlots = _appointmentService.GetAvailableTimeSlots(doctorId, date);
                return Json(timeSlots.Select(ts => ts.ToString(@"hh\:mm")));
            }
            catch
            {
                return Json(Enumerable.Empty<string>());
            }
        }

        [HttpGet]
        public IActionResult GetDoctorsByDepartment(int departmentId)
        {
            try
            {
                var doctors = _userService.GetDoctors()
                    .Where(d => d.DepartmentId == departmentId)
                    .OrderBy(d => d.FullName)
                    .Select(d => new { id = d.Id, fullName = d.FullName });
                
                return Json(doctors);
            }
            catch
            {
                return Json(Enumerable.Empty<object>());
            }
        }
    }
}


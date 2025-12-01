using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Data.Interfaces;

namespace HospitalAutomation.Web.Controllers
{
    [Authorize(Policy = "StaffOnly")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentController(
            IAppointmentService appointmentService,
            IPatientService patientService,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(DateTime? date = null)
        {
            try
            {
                var appointments = date.HasValue
                    ? _appointmentService.GetAppointmentsByDate(date.Value)
                    : _appointmentService.GetAllAppointments();

                ViewBag.SelectedDate = date;
                return View(appointments.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Randevular yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<Appointment>());
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
        public IActionResult Create(Appointment appointment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadViewBagData();
                    return View(appointment);
                }

                // Check time slot availability
                if (!_appointmentService.IsTimeSlotAvailable(appointment.DoctorId, appointment.AppointmentDate, appointment.AppointmentTime))
                {
                    ModelState.AddModelError("", "Bu zaman dilimi müsait değil!");
                    LoadViewBagData();
                    return View(appointment);
                }

                if (_appointmentService.CreateAppointment(appointment))
                {
                    TempData["SuccessMessage"] = "Randevu başarıyla oluşturuldu!";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Randevu oluşturulamadı");
                LoadViewBagData();
                return View(appointment);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                LoadViewBagData();
                return View(appointment);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var appointment = _appointmentService.GetAppointmentById(id);
                if (appointment == null)
                {
                    TempData["ErrorMessage"] = "Randevu bulunamadı";
                    return RedirectToAction("Index");
                }

                LoadViewBagData();
                return View(appointment);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Appointment appointment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadViewBagData();
                    return View(appointment);
                }

                if (_appointmentService.UpdateAppointment(appointment))
                {
                    TempData["SuccessMessage"] = "Randevu başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Randevu güncellenemedi");
                LoadViewBagData();
                return View(appointment);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                LoadViewBagData();
                return View(appointment);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id, string reason = null)
        {
            try
            {
                if (_appointmentService.CancelAppointment(id, reason ?? "İptal edildi"))
                {
                    TempData["SuccessMessage"] = "Randevu iptal edildi!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Randevu iptal edilemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Complete(int id)
        {
            try
            {
                if (_appointmentService.CompleteAppointment(id))
                {
                    TempData["SuccessMessage"] = "Randevu tamamlandı!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Randevu tamamlanamadı";
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
                var appointment = _appointmentService.GetAppointmentById(id);
                if (appointment == null)
                {
                    TempData["ErrorMessage"] = "Randevu bulunamadı";
                    return RedirectToAction("Index");
                }

                return View(appointment);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("Index");
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

        private void LoadViewBagData()
        {
            ViewBag.Patients = new SelectList(_patientService.GetAllPatients().OrderBy(p => p.FullName), "Id", "FullName");
            ViewBag.Doctors = new SelectList(_userService.GetDoctors().OrderBy(d => d.FullName), "Id", "FullName");
            ViewBag.Departments = new SelectList(_unitOfWork.Departments.GetActiveDepartments().OrderBy(d => d.Name), "Id", "Name");
        }
    }
}


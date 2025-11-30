using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;

namespace HospitalAutomation.Web.Controllers
{
    [Authorize(Policy = "StaffOnly")]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult DailyAppointments(DateTime? date = null)
        {
            try
            {
                var selectedDate = date ?? DateTime.Today;
                var appointments = _unitOfWork.Appointments.GetAppointmentsByDate(selectedDate);

                ViewBag.SelectedDate = selectedDate;
                return View(appointments.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Rapor yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<Appointment>());
            }
        }
    }
}


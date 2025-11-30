using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HospitalAutomation.Web.Models;
using HospitalAutomation.Utilities;
using HospitalAutomation.Data.Interfaces;

namespace HospitalAutomation.Web.Controllers
{
    [Authorize(Policy = "StaffOnly")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            try
            {
                ViewBag.UserName = SessionManager.GetDisplayName();
                ViewBag.UserType = SessionManager.GetUserType();
                
                // Get today's appointments count
                var todayAppointments = _unitOfWork.Appointments.GetTodayAppointments();
                ViewBag.TodayAppointmentsCount = todayAppointments.Count();
                
                // Get total patients count
                var totalPatients = _unitOfWork.Patients.GetAll();
                ViewBag.TotalPatientsCount = totalPatients.Count();
                
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard");
                ViewBag.ErrorMessage = "Dashboard yüklenirken hata oluştu";
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

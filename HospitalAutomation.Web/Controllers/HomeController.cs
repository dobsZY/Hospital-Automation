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
                var appointments = _unitOfWork.Appointments.GetAll().ToList();
                var todayAppointmentsCount = appointments.Count(a => a.AppointmentDate.Date == DateTime.Today);
                ViewBag.TodayAppointmentsCount = todayAppointmentsCount;
                
                // Get total patients count
                var totalPatients = _unitOfWork.Patients.GetAll();
                ViewBag.TotalPatientsCount = totalPatients.Count();

                // Chart 1: Appointments by Department (Top 5)
                var departmentStats = appointments
                    .Where(a => a.Doctor?.Department != null)
                    .GroupBy(a => a.Doctor.Department.Name)
                    .Select(g => new { Name = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .Take(5)
                    .ToList();

                ViewBag.DeptLabels = departmentStats.Select(x => x.Name).ToArray();
                ViewBag.DeptData = departmentStats.Select(x => x.Count).ToArray();

                // Chart 2: Last 7 Days Activity
                var last7Days = Enumerable.Range(0, 7).Select(i => DateTime.Today.AddDays(-6 + i)).ToList();
                var dailyStats = last7Days.Select(date => appointments.Count(a => a.AppointmentDate.Date == date)).ToArray();
                var dateLabels = last7Days.Select(d => d.ToString("dd MMM")).ToArray();

                ViewBag.DailyLabels = dateLabels;
                ViewBag.DailyData = dailyStats;
                
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

using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Utilities;
using HospitalAutomation.Web.ViewModels;
using HospitalAutomation.Data.Interfaces;

namespace HospitalAutomation.Web.Controllers
{
    [Authorize(Policy = "StaffOnly")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IUnitOfWork _unitOfWork;

        public PatientController(IPatientService patientService, IUnitOfWork unitOfWork)
        {
            _patientService = patientService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                var patients = string.IsNullOrWhiteSpace(searchTerm)
                    ? _patientService.GetAllPatients()
                    : _patientService.SearchPatients(searchTerm);

                ViewBag.SearchTerm = searchTerm;
                return View(patients.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hastalar yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<Patient>());
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
        public IActionResult Create(Patient patient)
        {
            Console.WriteLine(">>> Patient/Create [POST] Metoduna Girildi");
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    foreach (var error in errors)
                    {
                        Console.WriteLine($">>> ModelState Error: {error}");
                    }
                    
                    LoadViewBagData();
                    return View(patient);
                }

                if (_patientService.CreatePatient(patient))
                {
                    TempData["SuccessMessage"] = "Hasta başarıyla kaydedildi!";
                    return RedirectToAction("Create"); // Formu temizlemek için Create sayfasına yönlendir
                }

                ModelState.AddModelError("", "Hasta kaydedilemedi");
                LoadViewBagData();
                return View(patient);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                LoadViewBagData();
                return View(patient);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var patient = _patientService.GetPatientById(id);
                if (patient == null)
                {
                    TempData["ErrorMessage"] = "Hasta bulunamadı";
                    return RedirectToAction("Index");
                }

                LoadViewBagData();
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
        public IActionResult Edit(Patient patient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadViewBagData();
                    return View(patient);
                }

                if (_patientService.UpdatePatient(patient))
                {
                    TempData["SuccessMessage"] = "Hasta başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Hasta güncellenemedi");
                LoadViewBagData();
                return View(patient);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                LoadViewBagData();
                return View(patient);
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_patientService.DeletePatient(id))
                {
                    TempData["SuccessMessage"] = "Hasta başarıyla silindi!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Hasta silinemedi";
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
                var patient = _patientService.GetPatientById(id);
                if (patient == null)
                {
                    TempData["ErrorMessage"] = "Hasta bulunamadı";
                    return RedirectToAction("Index");
                }

                return View(patient);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult GetDistrictsByCity(int cityId)
        {
            try
            {
                var districts = _unitOfWork.Districts.GetDistrictsByCity(cityId);
                return Json(districts.Select(d => new { d.Id, d.Name }));
            }
            catch
            {
                return Json(Enumerable.Empty<object>());
            }
        }

        private void LoadViewBagData()
        {
            ViewBag.Cities = new SelectList(_unitOfWork.Cities.GetAll().Where(c => c.IsActive).OrderBy(c => c.Name), "Id", "Name");
            ViewBag.Districts = new SelectList(Enumerable.Empty<District>(), "Id", "Name");
        }
    }
}


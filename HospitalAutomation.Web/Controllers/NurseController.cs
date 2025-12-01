using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;
using HospitalAutomation.Services.Interfaces;

namespace HospitalAutomation.Web.Controllers
{
    [Authorize(Policy = "NurseOnly")]
    public class NurseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientService _patientService;

        public NurseController(IUnitOfWork unitOfWork, IPatientService patientService)
        {
            _unitOfWork = unitOfWork;
            _patientService = patientService;
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
                
                // Get today's vital signs count
                var todayVitalSigns = _unitOfWork.VitalSigns.GetVitalSignsByDate(DateTime.Today);
                ViewBag.TodayVitalSignsCount = todayVitalSigns.Count();
                
                // Get today's medications count
                var todayMedications = _unitOfWork.MedicationAdministrations.GetTodayMedications();
                ViewBag.TodayMedicationsCount = todayMedications.Count();

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Dashboard yüklenirken hata oluştu: {ex.Message}";
                return View();
            }
        }

        public IActionResult VitalSigns(int? patientId = null)
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var vitalSigns = patientId.HasValue
                    ? _unitOfWork.VitalSigns.GetVitalSignsByPatient(patientId.Value)
                    : _unitOfWork.VitalSigns.GetVitalSignsByNurse(currentUser.Id);

                ViewBag.PatientId = patientId;
                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                return View(vitalSigns.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Vital bulgular yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<VitalSigns>());
            }
        }

        [HttpGet]
        public IActionResult CreateVitalSigns(int? patientId = null)
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var vitalSigns = new VitalSigns
                {
                    NurseId = currentUser.Id,
                    PatientId = patientId ?? 0,
                    MeasurementDateTime = DateTime.Now
                };

                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                return View(vitalSigns);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("VitalSigns");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateVitalSigns(VitalSigns vitalSigns)
        {
            Console.WriteLine(">>> CreateVitalSigns [POST] Metoduna Girildi");
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    foreach (var error in errors)
                    {
                        Console.WriteLine($">>> VitalSigns ModelState Error: {error}");
                    }
                    ViewBag.Patients = _patientService.GetAllPatients().ToList();
                    return View(vitalSigns);
                }

                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser != null)
                {
                    // Verify user exists in DB to prevent Foreign Key errors (Stale Cookie)
                    var dbUser = _unitOfWork.Users.GetById(currentUser.Id);
                    if (dbUser == null)
                    {
                        return RedirectToAction("Logout", "Account");
                    }
                    vitalSigns.NurseId = currentUser.Id;
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                
                vitalSigns.MeasurementDateTime = DateTime.Now;
                vitalSigns.CreatedDate = DateTime.Now;
                vitalSigns.IsActive = true;
                
                // Veritabanı NOT NULL hatasını önlemek için varsayılan değerler
                vitalSigns.Notes ??= "";

                _unitOfWork.VitalSigns.Add(vitalSigns);
                _unitOfWork.Complete();

                TempData["SuccessMessage"] = "Vital bulgular başarıyla kaydedildi!";
                return RedirectToAction("VitalSigns");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">>> CreateVitalSigns Exception: {ex.Message}");
                if (ex.InnerException != null) Console.WriteLine($">>> Inner: {ex.InnerException.Message}");
                
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                return View(vitalSigns);
            }
        }

        public IActionResult MedicationTracking(int? patientId = null)
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var medications = patientId.HasValue
                    ? _unitOfWork.MedicationAdministrations.GetMedicationsByPatient(patientId.Value)
                    : _unitOfWork.MedicationAdministrations.GetMedicationsByNurse(currentUser.Id);

                ViewBag.PatientId = patientId;
                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                ViewBag.Medications = _unitOfWork.Medications.GetActiveMedications().ToList();
                return View(medications.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"İlaç takibi yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<MedicationAdministration>());
            }
        }

        [HttpGet]
        public IActionResult CreateMedicationAdministration(int? patientId = null)
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var administration = new MedicationAdministration
                {
                    NurseId = currentUser.Id,
                    PatientId = patientId ?? 0,
                    ScheduledDateTime = DateTime.Now,
                    Status = MedicationStatus.Scheduled
                };

                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                ViewBag.Medications = _unitOfWork.Medications.GetActiveMedications().ToList();
                ViewBag.Doctors = _unitOfWork.Users.GetByRole(UserRole.Doctor).ToList();
                return View(administration);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("MedicationTracking");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMedicationAdministration(MedicationAdministration administration)
        {
            Console.WriteLine(">>> CreateMedicationAdministration [POST] Metoduna Girildi");
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    foreach (var error in errors)
                    {
                        Console.WriteLine($">>> Medication ModelState Error: {error}");
                    }
                    ViewBag.Patients = _patientService.GetAllPatients().ToList();
                    ViewBag.Medications = _unitOfWork.Medications.GetActiveMedications().ToList();
                    ViewBag.Doctors = _unitOfWork.Users.GetByRole(UserRole.Doctor).ToList();
                    return View(administration);
                }

                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser != null)
                {
                    // Verify user exists in DB to prevent Foreign Key errors (Stale Cookie)
                    var dbUser = _unitOfWork.Users.GetById(currentUser.Id);
                    if (dbUser == null)
                    {
                        return RedirectToAction("Logout", "Account");
                    }
                    administration.NurseId = currentUser.Id;
                }
                else 
                {
                    return RedirectToAction("Login", "Account");
                }

                // DoctorId 0 gelirse null yap (FK hatasını önlemek için)
                if (administration.DoctorId == 0) administration.DoctorId = null;

                // MedicationId düzeltmesi
                if (administration.MedicationId == 0) 
                {
                     // Eğer MedicationId 0 ise, formda bir hata olabilir veya binding sorunu vardır.
                     // Medication.Id ve Medication.MedicationId uyuşmazlığı olabilir.
                     // View'da "value" attribute'u doğru set edilmemiş olabilir.
                }

                Console.WriteLine($"DEBUG: Saving MedicationAdministration: PatientId={administration.PatientId}, MedicationId={administration.MedicationId}, NurseId={administration.NurseId}, DoctorId={administration.DoctorId}");

                administration.CreatedDate = DateTime.Now;
                administration.IsActive = true;
                
                // Veritabanı NOT NULL hatasını önlemek için varsayılan değerler
                administration.SideEffects ??= "";
                administration.Notes ??= "";

                _unitOfWork.MedicationAdministrations.Add(administration);
                _unitOfWork.Complete();

                TempData["SuccessMessage"] = "İlaç uygulaması başarıyla kaydedildi!";
                return RedirectToAction("MedicationTracking");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">>> CreateMedication Exception: {ex.Message}");
                if (ex.InnerException != null) Console.WriteLine($">>> Inner: {ex.InnerException.Message}");

                ModelState.AddModelError("", $"Hata: {ex.Message}");
                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                ViewBag.Medications = _unitOfWork.Medications.GetActiveMedications().ToList();
                ViewBag.Doctors = _unitOfWork.Users.GetByRole(UserRole.Doctor).ToList();
                return View(administration);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdministerMedication(int id)
        {
            try
            {
                // Use SingleOrDefault with AdministrationId because GetById looks for Id which might be 0
                var administration = _unitOfWork.MedicationAdministrations.SingleOrDefault(x => x.AdministrationId == id);
                if (administration == null)
                {
                    TempData["ErrorMessage"] = "İlaç uygulaması bulunamadı";
                    return RedirectToAction("MedicationTracking");
                }

                administration.Status = MedicationStatus.Administered;
                administration.AdministeredDateTime = DateTime.Now;
                administration.UpdatedDate = DateTime.Now;

                _unitOfWork.MedicationAdministrations.Update(administration);
                _unitOfWork.Complete();

                TempData["SuccessMessage"] = "İlaç uygulaması kaydedildi!";
                return RedirectToAction("MedicationTracking");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("MedicationTracking");
            }
        }

        public IActionResult NursingNotes(int? patientId = null)
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var notes = patientId.HasValue
                    ? _unitOfWork.NursingNotes.GetNotesByPatient(patientId.Value)
                    : _unitOfWork.NursingNotes.GetNotesByNurse(currentUser.Id);

                ViewBag.PatientId = patientId;
                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                return View(notes.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hemşire notları yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<NursingNote>());
            }
        }

        [HttpGet]
        public IActionResult CreateNursingNote(int? patientId = null)
        {
            try
            {
                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var note = new NursingNote
                {
                    NurseId = currentUser.Id,
                    PatientId = patientId ?? 0,
                    NoteDateTime = DateTime.Now
                };

                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                return View(note);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("NursingNotes");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNursingNote(NursingNote note)
        {
            Console.WriteLine(">>> CreateNursingNote [POST] Metoduna Girildi");
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    foreach (var error in errors)
                    {
                        Console.WriteLine($">>> Note ModelState Error: {error}");
                    }
                    ViewBag.Patients = _patientService.GetAllPatients().ToList();
                    return View(note);
                }

                var currentUser = SessionManager.GetCurrentUser(HttpContext);
                if (currentUser != null)
                {
                    // Verify user exists in DB to prevent Foreign Key errors (Stale Cookie)
                    var dbUser = _unitOfWork.Users.GetById(currentUser.Id);
                    if (dbUser == null)
                    {
                        return RedirectToAction("Logout", "Account");
                    }
                    note.NurseId = currentUser.Id;
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }

                note.NoteDateTime = DateTime.Now;
                note.CreatedDate = DateTime.Now;
                note.IsActive = true;
                
                // Veritabanı NOT NULL hatasını önlemek için varsayılan değerler
                note.Assessment ??= "";
                note.InterventionPlanned ??= "";
                note.PatientResponse ??= "";

                _unitOfWork.NursingNotes.Add(note);
                _unitOfWork.Complete();

                TempData["SuccessMessage"] = "Hemşire notu başarıyla kaydedildi!";
                return RedirectToAction("NursingNotes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">>> CreateNote Exception: {ex.Message}");
                if (ex.InnerException != null) Console.WriteLine($">>> Inner: {ex.InnerException.Message}");

                ModelState.AddModelError("", $"Hata: {ex.Message}");
                ViewBag.Patients = _patientService.GetAllPatients().ToList();
                return View(note);
            }
        }

        public IActionResult Patients()
        {
            try
            {
                var patients = _patientService.GetAllPatients();
                return View(patients.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hastalar yüklenirken hata oluştu: {ex.Message}";
                return View(Enumerable.Empty<Patient>());
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

                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}


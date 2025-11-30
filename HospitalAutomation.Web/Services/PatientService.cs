using System;
using System.Collections.Generic;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public bool CreatePatient(Patient patient)
        {
            try
            {
                if (patient == null)
                    throw new ArgumentNullException(nameof(patient), "Hasta bilgileri boş olamaz");

                // Authorization centralised
                AuthorizationHelper.EnsureCanCreatePatient();

                // Validate required patient data
                ValidatePatientData(patient);

                // Check if national ID already exists
                if (_unitOfWork.Patients.IsNationalIdExists(patient.NationalId))
                    throw new InvalidOperationException("Bu TC Kimlik No ile kayıtlı hasta bulunmaktadır");

                // Set default values
                patient.CreatedDate = DateTime.Now;
                patient.IsActive = true;
                if (string.IsNullOrWhiteSpace(patient.CreatedBy))
                    patient.CreatedBy = SessionManager.IsLoggedIn ? SessionManager.GetDisplayName() : "System";

                // Add patient to repository
                _unitOfWork.Patients.Add(patient);

                // Save changes
                var result = _unitOfWork.Complete();
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CreatePatient Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");

                throw new Exception($"Hasta oluşturulurken hata oluştu: {ex.Message}", ex);
            }
        }

        public bool UpdatePatient(Patient patient)
        {
            try
            {
                if (patient == null)
                    throw new ArgumentNullException(nameof(patient), "Hasta bilgileri boş olamaz");

                // Centralized authorization
                AuthorizationHelper.EnsureCanModifyPatient(patient.Id);

                // Validate patient data
                ValidatePatientData(patient);

                var existingPatient = _unitOfWork.Patients.GetById(patient.Id);
                if (existingPatient == null)
                    throw new InvalidOperationException("Güncellenecek hasta bulunamadı");

                // Check if national ID is being changed and if it already exists
                if (existingPatient.NationalId != patient.NationalId &&
                    _unitOfWork.Patients.IsNationalIdExists(patient.NationalId))
                    throw new InvalidOperationException("Bu TC Kimlik No ile kayıtlı başka bir hasta bulunmaktadır");

                // Update patient
                patient.UpdatedDate = DateTime.Now;
                if (string.IsNullOrWhiteSpace(patient.UpdatedBy))
                    patient.UpdatedBy = SessionManager.IsLoggedIn ? SessionManager.GetDisplayName() : "System";

                _unitOfWork.Patients.Update(patient);
                var result = _unitOfWork.Complete();
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdatePatient Error: {ex.Message}");
                throw new Exception($"Hasta güncellenirken hata oluştu: {ex.Message}", ex);
            }
        }

        public bool DeletePatient(int patientId)
        {
            try
            {
                if (patientId <= 0)
                    throw new ArgumentException("Geçersiz hasta ID", nameof(patientId));

                // Only Admin hasta silebilir
                AuthorizationHelper.EnsureAdmin();

                var patient = _unitOfWork.Patients.GetById(patientId);
                if (patient == null)
                    return false;

                // Soft delete
                _unitOfWork.Patients.Remove(patient);
                var result = _unitOfWork.Complete();
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeletePatient Error: {ex.Message}");
                throw new Exception($"Hasta silinirken hata oluştu: {ex.Message}", ex);
            }
        }

        public Patient GetPatientById(int patientId)
        {
            try
            {
                if (patientId <= 0)
                    return null;

                return _unitOfWork.Patients.GetById(patientId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetPatientById Error: {ex.Message}");
                throw new Exception($"Hasta getirilirken hata oluştu: {ex.Message}", ex);
            }
        }

        public Patient GetPatientByNationalId(string nationalId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nationalId))
                    return null;

                return _unitOfWork.Patients.GetByNationalId(nationalId.Trim());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetPatientByNationalId Error: {ex.Message}");
                throw new Exception($"Hasta getirilirken hata oluştu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Patients.GetAll();
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllPatients Error: {ex.Message}");
                throw new Exception($"Hastalar getirilirken hata oluştu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Patient> SearchPatients(string searchTerm)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();

                if (string.IsNullOrWhiteSpace(searchTerm))
                    return GetAllPatients();

                return _unitOfWork.Patients.SearchPatients(searchTerm.Trim());
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchPatients Error: {ex.Message}");
                throw new Exception($"Hasta arama sırasında hata oluştu: {ex.Message}", ex);
            }
        }

        public bool IsNationalIdExists(string nationalId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nationalId))
                    return false;

                return _unitOfWork.Patients.IsNationalIdExists(nationalId.Trim());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"IsNationalIdExists Error: {ex.Message}");
                throw new Exception($"TC Kimlik No kontrolü sırasında hata oluştu: {ex.Message}", ex);
            }
        }

        private void ValidatePatientData(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.NationalId))
                throw new ArgumentException("TC Kimlik No boş olamaz");

            if (patient.NationalId.Length != 11)
                throw new ArgumentException("TC Kimlik No 11 karakter olmalıdır");

            if (string.IsNullOrWhiteSpace(patient.FirstName))
                throw new ArgumentException("Ad boş olamaz");

            if (string.IsNullOrWhiteSpace(patient.LastName))
                throw new ArgumentException("Soyad boş olamaz");

            if (patient.BirthDate == default(DateTime))
                throw new ArgumentException("Doğum tarihi belirtilmelidir");

            if (patient.BirthDate >= DateTime.Today)
                throw new ArgumentException("Doğum tarihi bugünden önce olmalıdır");

            var age = DateTime.Today.Year - patient.BirthDate.Year;
            if (DateTime.Today.DayOfYear < patient.BirthDate.DayOfYear)
                age--;

            if (age < 0 || age > 120)
                throw new ArgumentException("Geçerli bir doğum tarihi giriniz");
        }
    }
}


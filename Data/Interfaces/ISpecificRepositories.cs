using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalAutomation.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsername(string username);
        User GetByEmail(string email);
        User Authenticate(string username, string password);
        IEnumerable<User> GetByRole(UserRole role);
        IEnumerable<User> GetDoctors();
        IEnumerable<User> GetDoctorsByDepartment(int departmentId);
        bool IsUsernameExists(string username);
        bool IsEmailExists(string email);
    }

    public interface IPatientRepository : IRepository<Patient>
    {
        Patient GetByNationalId(string nationalId);
        IEnumerable<Patient> SearchPatients(string searchTerm);
        IEnumerable<Patient> GetPatientsByCity(int cityId);
        IEnumerable<Patient> GetPatientsByDistrict(int districtId);
        bool IsNationalIdExists(string nationalId);
        IEnumerable<Patient> GetPatientsByAgeRange(int minAge, int maxAge);
        IEnumerable<Patient> GetPatientsByGender(Gender gender);
    }

    public interface IDepartmentRepository : IRepository<Department>
    {
        Department GetByCode(string code);
        IEnumerable<Department> GetActiveDepartments();
        Department GetDepartmentWithDoctors(int departmentId);
        IEnumerable<Department> GetDepartmentsWithDoctorCount();
    }

    public interface IAppointmentRepository : IRepository<Appointment>
    {
        IEnumerable<Appointment> GetAppointmentsByPatient(int patientId);
        IEnumerable<Appointment> GetAppointmentsByDoctor(int doctorId);
        IEnumerable<Appointment> GetAppointmentsByDate(DateTime date);
        IEnumerable<Appointment> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<Appointment> GetTodayAppointments();
        IEnumerable<Appointment> GetUpcomingAppointments();
        IEnumerable<Appointment> GetAppointmentsByStatus(AppointmentStatus status);
        bool HasConflictingAppointment(int doctorId, DateTime appointmentDate, TimeSpan appointmentTime, int? excludeAppointmentId = null);
        bool IsTimeSlotAvailable(int doctorId, DateTime date, TimeSpan time, int? excludeAppointmentId = null);
        IEnumerable<Appointment> GetByPatientId(int patientId);
        IEnumerable<Appointment> GetByDoctorId(int doctorId);
        IEnumerable<Appointment> GetByDate(DateTime date);
        IEnumerable<Appointment> GetByDateRange(DateTime startDate, DateTime endDate);
    }

    public interface IMedicalRecordRepository : IRepository<MedicalRecord>
    {
        IEnumerable<MedicalRecord> GetRecordsByPatient(int patientId);
        IEnumerable<MedicalRecord> GetRecordsByDoctor(int doctorId);
        IEnumerable<MedicalRecord> GetRecordsByDateRange(DateTime startDate, DateTime endDate);
        MedicalRecord GetRecordByAppointment(int appointmentId);
        IEnumerable<MedicalRecord> SearchByDiagnosis(string diagnosis);
    }

    public interface ICityRepository : IRepository<City>
    {
        IEnumerable<City> GetCitiesWithDistricts();
        City GetCityWithDistricts(int cityId);
        IEnumerable<City> GetCitiesByRegion(string region);
    }

    public interface IDistrictRepository : IRepository<District>
    {
        IEnumerable<District> GetDistrictsByCity(int cityId);
        IEnumerable<District> GetDistrictsWithCity();
    }

    // Hemþire iþlemleri için yeni repository interface'leri
    public interface IVitalSignsRepository : IRepository<VitalSigns>
    {
        IEnumerable<VitalSigns> GetVitalSignsByPatient(int patientId);
        IEnumerable<VitalSigns> GetVitalSignsByNurse(int nurseId);
        IEnumerable<VitalSigns> GetVitalSignsByDateRange(DateTime startDate, DateTime endDate);
        VitalSigns GetLatestVitalSigns(int patientId);
        IEnumerable<VitalSigns> GetVitalSignsByDate(DateTime date);
    }

    public interface IMedicationRepository : IRepository<Medication>
    {
        IEnumerable<Medication> GetActiveMedications();
        IEnumerable<Medication> SearchMedications(string searchTerm);
        Medication GetByName(string name);
    }

    public interface IMedicationAdministrationRepository : IRepository<MedicationAdministration>
    {
        IEnumerable<MedicationAdministration> GetMedicationsByPatient(int patientId);
        IEnumerable<MedicationAdministration> GetMedicationsByNurse(int nurseId);
        IEnumerable<MedicationAdministration> GetMedicationsByStatus(MedicationStatus status);
        IEnumerable<MedicationAdministration> GetMedicationsByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<MedicationAdministration> GetScheduledMedications(DateTime date);
        IEnumerable<MedicationAdministration> GetOverdueMedications();
        IEnumerable<MedicationAdministration> GetTodayMedications();
    }

    public interface INursingNoteRepository : IRepository<NursingNote>
    {
        IEnumerable<NursingNote> GetNotesByPatient(int patientId);
        IEnumerable<NursingNote> GetNotesByNurse(int nurseId);
        IEnumerable<NursingNote> GetNotesByType(NursingNoteType noteType);
        IEnumerable<NursingNote> GetUrgentNotes();
        IEnumerable<NursingNote> GetNotesByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<NursingNote> GetTodayNotes();
        IEnumerable<NursingNote> GetRecentNotes(int count = 10);
    }
}
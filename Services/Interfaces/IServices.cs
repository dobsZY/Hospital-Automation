using System;
using System.Collections.Generic;
using HospitalAutomation.Models;

namespace HospitalAutomation.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        bool CreateUser(User user, string password);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetDoctors();
        IEnumerable<User> GetUsersByRole(Models.Enums.UserRole role);
        bool IsUsernameExists(string username);
        bool IsEmailExists(string email);
        bool ChangePassword(int userId, string oldPassword, string newPassword);
    }

    public interface IPatientService
    {
        bool CreatePatient(Patient patient);
        bool UpdatePatient(Patient patient);
        bool DeletePatient(int patientId);
        Patient GetPatientById(int patientId);
        Patient GetPatientByNationalId(string nationalId);
        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Patient> SearchPatients(string searchTerm);
        bool IsNationalIdExists(string nationalId);
    }

    public interface IAppointmentService
    {
        bool CreateAppointment(Appointment appointment);
        bool UpdateAppointment(Appointment appointment);
        bool CancelAppointment(int appointmentId, string reason);
        bool CompleteAppointment(int appointmentId);
        Appointment GetAppointmentById(int appointmentId);
        IEnumerable<Appointment> GetAllAppointments();
        IEnumerable<Appointment> GetAppointmentsByPatient(int patientId);
        IEnumerable<Appointment> GetAppointmentsByDoctor(int doctorId);
        IEnumerable<Appointment> GetAppointmentsByDate(DateTime date);
        IEnumerable<Appointment> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate);
        bool IsTimeSlotAvailable(int doctorId, DateTime date, TimeSpan time);
        IEnumerable<TimeSpan> GetAvailableTimeSlots(int doctorId, DateTime date);
    }

    public interface IDepartmentService
    {
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(int departmentId);
        Department GetDepartmentById(int departmentId);
        IEnumerable<Department> GetAllDepartments();
        bool IsCodeExists(string code);
    }

    public interface IMedicalRecordService
    {
        bool CreateMedicalRecord(MedicalRecord record);
        bool UpdateMedicalRecord(MedicalRecord record);
        bool DeleteMedicalRecord(int recordId);
        MedicalRecord GetMedicalRecordById(int recordId);
        IEnumerable<MedicalRecord> GetMedicalRecordsByPatient(int patientId);
        IEnumerable<MedicalRecord> GetMedicalRecordsByDoctor(int doctorId);
        IEnumerable<MedicalRecord> GetMedicalRecordsByDateRange(DateTime startDate, DateTime endDate);
    }
}
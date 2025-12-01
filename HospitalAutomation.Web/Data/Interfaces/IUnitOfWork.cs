using System;
using HospitalAutomation.Data.Repositories;

namespace HospitalAutomation.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IPatientRepository Patients { get; }
        IDepartmentRepository Departments { get; }
        IAppointmentRepository Appointments { get; }
        IMedicalRecordRepository MedicalRecords { get; }
        ICityRepository Cities { get; }
        IDistrictRepository Districts { get; }
        
        // Hemşire işlemleri için yeni repository'ler
        IVitalSignsRepository VitalSigns { get; }
        IMedicationRepository Medications { get; }
        IMedicationAdministrationRepository MedicationAdministrations { get; }
        INursingNoteRepository NursingNotes { get; }

        int Complete();
        System.Threading.Tasks.Task<int> CompleteAsync();
        int Save();
        System.Threading.Tasks.Task<int> SaveAsync();
        int SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}


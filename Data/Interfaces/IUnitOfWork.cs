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

        int Complete();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
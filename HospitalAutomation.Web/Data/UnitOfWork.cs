using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Data.Repositories;

namespace HospitalAutomation.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HospitalDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(HospitalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Users = new UserRepository(_context);
            Patients = new PatientRepository(_context);
            Departments = new DepartmentRepository(_context);
            Appointments = new AppointmentRepository(_context);
            MedicalRecords = new MedicalRecordRepository(_context);
            Cities = new CityRepository(_context);
            Districts = new DistrictRepository(_context);
            
            // Hemşire işlemleri için yeni repository'ler
            VitalSigns = new VitalSignsRepository(_context);
            Medications = new MedicationRepository(_context);
            MedicationAdministrations = new MedicationAdministrationRepository(_context);
            NursingNotes = new NursingNoteRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IPatientRepository Patients { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IMedicalRecordRepository MedicalRecords { get; private set; }
        public ICityRepository Cities { get; private set; }
        public IDistrictRepository Districts { get; private set; }
        
        // Hemşire işlemleri için yeni repository'ler
        public IVitalSignsRepository VitalSigns { get; private set; }
        public IMedicationRepository Medications { get; private set; }
        public IMedicationAdministrationRepository MedicationAdministrations { get; private set; }
        public INursingNoteRepository NursingNotes { get; private set; }

        // Property isimleri uyumluluğu için
        public IUserRepository UserRepository => Users;
        public IPatientRepository PatientRepository => Patients;
        public IDepartmentRepository DepartmentRepository => Departments;
        public IAppointmentRepository AppointmentRepository => Appointments;
        public IMedicalRecordRepository MedicalRecordRepository => MedicalRecords;
        public ICityRepository CityRepository => Cities;
        public IDistrictRepository DistrictRepository => Districts;
        
        // Hemşire işlemleri için property'ler
        public IVitalSignsRepository VitalSignsRepository => VitalSigns;
        public IMedicationRepository MedicationRepository => Medications;
        public IMedicationAdministrationRepository MedicationAdministrationRepository => MedicationAdministrations;
        public INursingNoteRepository NursingNoteRepository => NursingNotes;

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                // EF update hatalarını ayırt edip loglayalım
                var innerMessage = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                throw new Exception($"Veritabanı hatası: {innerMessage}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Veritabanı kaydetme işlemi sırasında hata oluştu.", ex);
            }
        }

        public int Save()
        {
            return Complete();
        }

        public int SaveChanges()
        {
            return Complete();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}


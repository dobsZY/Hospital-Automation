using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;

namespace HospitalAutomation.Data.Repositories
{
    public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(HospitalDbContext context) : base(context)
        {
        }

        // Interface methods implementation
        public IEnumerable<MedicalRecord> GetRecordsByPatient(int patientId)
        {
            return _dbSet.Include(mr => mr.Patient)
                        .Include(mr => mr.Doctor)
                        .Include(mr => mr.Appointment)
                        .Where(mr => mr.PatientId == patientId && mr.IsActive)
                        .OrderByDescending(mr => mr.RecordDate)
                        .ToList();
        }

        public IEnumerable<MedicalRecord> GetRecordsByDoctor(int doctorId)
        {
            return _dbSet.Include(mr => mr.Patient)
                        .Include(mr => mr.Doctor)
                        .Include(mr => mr.Appointment)
                        .Where(mr => mr.DoctorId == doctorId && mr.IsActive)
                        .OrderByDescending(mr => mr.RecordDate)
                        .ToList();
        }

        public IEnumerable<MedicalRecord> GetRecordsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dbSet.Include(mr => mr.Patient)
                        .Include(mr => mr.Doctor)
                        .Include(mr => mr.Appointment)
                        .Where(mr => mr.RecordDate.Date >= startDate.Date && 
                                    mr.RecordDate.Date <= endDate.Date && mr.IsActive)
                        .OrderByDescending(mr => mr.RecordDate)
                        .ToList();
        }

        public MedicalRecord GetRecordByAppointment(int appointmentId)
        {
            return _dbSet.Include(mr => mr.Patient)
                        .Include(mr => mr.Doctor)
                        .Include(mr => mr.Appointment)
                        .FirstOrDefault(mr => mr.AppointmentId == appointmentId && mr.IsActive);
        }

        public IEnumerable<MedicalRecord> SearchByDiagnosis(string diagnosis)
        {
            if (string.IsNullOrWhiteSpace(diagnosis))
                return new List<MedicalRecord>();

            var searchTerm = diagnosis.ToLower().Trim();
            return _dbSet.Include(mr => mr.Patient)
                        .Include(mr => mr.Doctor)
                        .Include(mr => mr.Appointment)
                        .Where(mr => mr.IsActive && 
                                    (mr.Diagnosis.ToLower().Contains(searchTerm) ||
                                     mr.Symptoms.ToLower().Contains(searchTerm) ||
                                     (mr.Notes != null && mr.Notes.ToLower().Contains(searchTerm))))
                        .OrderByDescending(mr => mr.RecordDate)
                        .ToList();
        }

        // Keep existing methods for backward compatibility
        public IEnumerable<MedicalRecord> GetByPatientId(int patientId)
        {
            return GetRecordsByPatient(patientId);
        }

        public IEnumerable<MedicalRecord> GetByDoctorId(int doctorId)
        {
            return GetRecordsByDoctor(doctorId);
        }

        public IEnumerable<MedicalRecord> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return GetRecordsByDateRange(startDate, endDate);
        }
    }
}
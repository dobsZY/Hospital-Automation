using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Data.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalDbContext context) : base(context)
        {
        }

        public override IEnumerable<Appointment> GetAll()
        {
            return _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.IsActive)
                        .ToList();
        }

        public override Appointment GetById(int id)
        {
            return _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .FirstOrDefault(a => a.Id == id && a.IsActive);
        }

        // Interface methods implementation
        public IEnumerable<Appointment> GetAppointmentsByPatient(int patientId)
        {
            return _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.PatientId == patientId && a.IsActive)
                        .OrderByDescending(a => a.AppointmentDate)
                        .ThenByDescending(a => a.AppointmentTime)
                        .ToList();
        }

        public IEnumerable<Appointment> GetAppointmentsByDoctor(int doctorId)
        {
            return _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.DoctorId == doctorId && a.IsActive)
                        .OrderByDescending(a => a.AppointmentDate)
                        .ThenByDescending(a => a.AppointmentTime)
                        .ToList();
        }

        public IEnumerable<Appointment> GetAppointmentsByDate(DateTime date)
        {
            return _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.AppointmentDate.Date == date.Date && a.IsActive)
                        .OrderBy(a => a.AppointmentTime)
                        .ToList();
        }

        public IEnumerable<Appointment> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.AppointmentDate.Date >= startDate.Date && 
                               a.AppointmentDate.Date <= endDate.Date && a.IsActive)
                        .OrderBy(a => a.AppointmentDate)
                        .ThenBy(a => a.AppointmentTime)
                        .ToList();
        }

        public IEnumerable<Appointment> GetTodayAppointments()
        {
            var today = DateTime.Today;
            return GetAppointmentsByDate(today);
        }

        public IEnumerable<Appointment> GetUpcomingAppointments()
        {
            var today = DateTime.Today;
            return _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.AppointmentDate >= today && a.IsActive)
                        .OrderBy(a => a.AppointmentDate)
                        .ThenBy(a => a.AppointmentTime)
                        .ToList();
        }

        public IEnumerable<Appointment> GetAppointmentsByStatus(AppointmentStatus status)
        {
            return _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.Status == status && a.IsActive)
                        .OrderBy(a => a.AppointmentDate)
                        .ThenBy(a => a.AppointmentTime)
                        .ToList();
        }

        public bool HasConflictingAppointment(int doctorId, DateTime appointmentDate, TimeSpan appointmentTime, int? excludeAppointmentId = null)
        {
            var query = _dbSet.Where(a => a.DoctorId == doctorId &&
                                         a.AppointmentDate.Date == appointmentDate.Date &&
                                         a.AppointmentTime == appointmentTime &&
                                         a.IsActive &&
                                         a.Status != AppointmentStatus.Cancelled);
            
            if (excludeAppointmentId.HasValue)
            {
                query = query.Where(a => a.Id != excludeAppointmentId.Value);
            }

            return query.Any();
        }

        // Keep existing methods for backward compatibility
        public IEnumerable<Appointment> GetByPatientId(int patientId)
        {
            return GetAppointmentsByPatient(patientId);
        }

        public IEnumerable<Appointment> GetByDoctorId(int doctorId)
        {
            return GetAppointmentsByDoctor(doctorId);
        }

        public IEnumerable<Appointment> GetByDate(DateTime date)
        {
            return GetAppointmentsByDate(date);
        }

        public IEnumerable<Appointment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return GetAppointmentsByDateRange(startDate, endDate);
        }

        public bool IsTimeSlotAvailable(int doctorId, DateTime date, TimeSpan time)
        {
            return !HasConflictingAppointment(doctorId, date, time);
        }

        public bool IsTimeSlotAvailable(int doctorId, DateTime date, TimeSpan time, int? excludeAppointmentId = null)
        {
            return !HasConflictingAppointment(doctorId, date, time, excludeAppointmentId);
        }
    }
}
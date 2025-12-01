using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
            var appointments = _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.PatientId == patientId && a.IsActive)
                        .ToList();

            return appointments.OrderByDescending(a => a.AppointmentDate)
                        .ThenByDescending(a => a.AppointmentTime);
        }

        public IEnumerable<Appointment> GetAppointmentsByDoctor(int doctorId)
        {
            var appointments = _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.DoctorId == doctorId && a.IsActive)
                        .ToList();

            return appointments.OrderByDescending(a => a.AppointmentDate)
                        .ThenByDescending(a => a.AppointmentTime);
        }

        public IEnumerable<Appointment> GetAppointmentsByDate(DateTime date)
        {
            var appointments = _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.AppointmentDate.Date == date.Date && a.IsActive)
                        .ToList();

            return appointments.OrderBy(a => a.AppointmentTime);
        }

        public IEnumerable<Appointment> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate)
        {
            var appointments = _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.AppointmentDate.Date >= startDate.Date && 
                            a.AppointmentDate.Date <= endDate.Date && a.IsActive)
                        .ToList();

            return appointments.OrderBy(a => a.AppointmentDate)
                        .ThenBy(a => a.AppointmentTime);
        }

        public IEnumerable<Appointment> GetTodayAppointments()
        {
            var today = DateTime.Today;
            return GetAppointmentsByDate(today);
        }

        public IEnumerable<Appointment> GetUpcomingAppointments()
        {
            var today = DateTime.Today;
            var appointments = _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.AppointmentDate >= today && a.IsActive)
                        .ToList();

            return appointments.OrderBy(a => a.AppointmentDate)
                        .ThenBy(a => a.AppointmentTime);
        }

        public IEnumerable<Appointment> GetAppointmentsByStatus(AppointmentStatus status)
        {
            var appointments = _dbSet.Include(a => a.Patient)
                        .Include(a => a.Doctor)
                        .Include(a => a.Department)
                        .Where(a => a.Status == status && a.IsActive)
                        .ToList();

            return appointments.OrderBy(a => a.AppointmentDate)
                        .ThenBy(a => a.AppointmentTime);
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


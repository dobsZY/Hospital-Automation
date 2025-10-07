using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Data.Repositories
{
    public class MedicationAdministrationRepository : Repository<MedicationAdministration>, IMedicationAdministrationRepository
    {
        public MedicationAdministrationRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<MedicationAdministration> GetMedicationsByPatient(int patientId)
        {
            return _context.Set<MedicationAdministration>()
                .Include(ma => ma.Patient)
                .Include(ma => ma.Medication)
                .Include(ma => ma.Nurse)
                .Include(ma => ma.Doctor)
                .Where(ma => ma.PatientId == patientId)
                .OrderByDescending(ma => ma.ScheduledDateTime);
        }

        public IEnumerable<MedicationAdministration> GetMedicationsByNurse(int nurseId)
        {
            return _context.Set<MedicationAdministration>()
                .Include(ma => ma.Patient)
                .Include(ma => ma.Medication)
                .Include(ma => ma.Nurse)
                .Include(ma => ma.Doctor)
                .Where(ma => ma.NurseId == nurseId)
                .OrderByDescending(ma => ma.ScheduledDateTime);
        }

        public IEnumerable<MedicationAdministration> GetMedicationsByStatus(MedicationStatus status)
        {
            return _context.Set<MedicationAdministration>()
                .Include(ma => ma.Patient)
                .Include(ma => ma.Medication)
                .Include(ma => ma.Nurse)
                .Include(ma => ma.Doctor)
                .Where(ma => ma.Status == status)
                .OrderByDescending(ma => ma.ScheduledDateTime);
        }

        public IEnumerable<MedicationAdministration> GetMedicationsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Set<MedicationAdministration>()
                .Include(ma => ma.Patient)
                .Include(ma => ma.Medication)
                .Include(ma => ma.Nurse)
                .Include(ma => ma.Doctor)
                .Where(ma => ma.ScheduledDateTime >= startDate && ma.ScheduledDateTime <= endDate)
                .OrderByDescending(ma => ma.ScheduledDateTime);
        }

        public IEnumerable<MedicationAdministration> GetScheduledMedications(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            return _context.Set<MedicationAdministration>()
                .Include(ma => ma.Patient)
                .Include(ma => ma.Medication)
                .Include(ma => ma.Nurse)
                .Include(ma => ma.Doctor)
                .Where(ma => ma.ScheduledDateTime >= startDate && 
                            ma.ScheduledDateTime < endDate &&
                            ma.Status == MedicationStatus.Scheduled)
                .OrderBy(ma => ma.ScheduledDateTime);
        }

        public IEnumerable<MedicationAdministration> GetOverdueMedications()
        {
            var now = DateTime.Now;

            return _context.Set<MedicationAdministration>()
                .Include(ma => ma.Patient)
                .Include(ma => ma.Medication)
                .Include(ma => ma.Nurse)
                .Include(ma => ma.Doctor)
                .Where(ma => ma.ScheduledDateTime < now && 
                            ma.Status == MedicationStatus.Scheduled)
                .OrderBy(ma => ma.ScheduledDateTime);
        }

        public IEnumerable<MedicationAdministration> GetTodayMedications()
        {
            return GetScheduledMedications(DateTime.Today);
        }
    }
}
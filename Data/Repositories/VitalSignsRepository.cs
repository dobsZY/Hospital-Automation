using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;

namespace HospitalAutomation.Data.Repositories
{
    public class VitalSignsRepository : Repository<VitalSigns>, IVitalSignsRepository
    {
        public VitalSignsRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<VitalSigns> GetVitalSignsByPatient(int patientId)
        {
            return _context.Set<VitalSigns>()
                .Include(vs => vs.Patient)
                .Include(vs => vs.Nurse)
                .Where(vs => vs.PatientId == patientId)
                .OrderByDescending(vs => vs.MeasurementDateTime);
        }

        public IEnumerable<VitalSigns> GetVitalSignsByNurse(int nurseId)
        {
            return _context.Set<VitalSigns>()
                .Include(vs => vs.Patient)
                .Include(vs => vs.Nurse)
                .Where(vs => vs.NurseId == nurseId)
                .OrderByDescending(vs => vs.MeasurementDateTime);
        }

        public IEnumerable<VitalSigns> GetVitalSignsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Set<VitalSigns>()
                .Include(vs => vs.Patient)
                .Include(vs => vs.Nurse)
                .Where(vs => vs.MeasurementDateTime >= startDate && vs.MeasurementDateTime <= endDate)
                .OrderByDescending(vs => vs.MeasurementDateTime);
        }

        public VitalSigns GetLatestVitalSigns(int patientId)
        {
            return _context.Set<VitalSigns>()
                .Include(vs => vs.Patient)
                .Include(vs => vs.Nurse)
                .Where(vs => vs.PatientId == patientId)
                .OrderByDescending(vs => vs.MeasurementDateTime)
                .FirstOrDefault();
        }

        public IEnumerable<VitalSigns> GetVitalSignsByDate(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            return _context.Set<VitalSigns>()
                .Include(vs => vs.Patient)
                .Include(vs => vs.Nurse)
                .Where(vs => vs.MeasurementDateTime >= startDate && vs.MeasurementDateTime < endDate)
                .OrderByDescending(vs => vs.MeasurementDateTime);
        }
    }
}
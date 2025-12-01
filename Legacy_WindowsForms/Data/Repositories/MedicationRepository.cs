using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;

namespace HospitalAutomation.Data.Repositories
{
    public class MedicationRepository : Repository<Medication>, IMedicationRepository
    {
        public MedicationRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<Medication> GetActiveMedications()
        {
            return _context.Set<Medication>()
                .Where(m => m.IsActive)
                .OrderBy(m => m.Name);
        }

        public IEnumerable<Medication> SearchMedications(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetActiveMedications();

            searchTerm = searchTerm.ToLower();

            return _context.Set<Medication>()
                .Where(m => m.IsActive && 
                           (m.Name.ToLower().Contains(searchTerm) ||
                            m.Description.ToLower().Contains(searchTerm) ||
                            m.Manufacturer.ToLower().Contains(searchTerm)))
                .OrderBy(m => m.Name);
        }

        public Medication GetByName(string name)
        {
            return _context.Set<Medication>()
                .FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && m.IsActive);
        }
    }
}
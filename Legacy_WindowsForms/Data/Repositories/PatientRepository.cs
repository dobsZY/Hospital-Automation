using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Data.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalDbContext context) : base(context)
        {
        }

        public Patient GetByNationalId(string nationalId)
        {
            return _dbSet.FirstOrDefault(p => p.NationalId == nationalId && p.IsActive);
        }

        public bool IsNationalIdExists(string nationalId)
        {
            return _dbSet.Any(p => p.NationalId == nationalId && p.IsActive);
        }

        public IEnumerable<Patient> SearchPatients(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAll();

            var term = searchTerm.ToLower().Trim();
            return _dbSet.Where(p => p.IsActive &&
                                    (p.FirstName.ToLower().Contains(term) ||
                                     p.LastName.ToLower().Contains(term) ||
                                     p.NationalId.Contains(term) ||
                                     (p.Phone != null && p.Phone.Contains(term)) ||
                                     (p.Email != null && p.Email.ToLower().Contains(term))))
                          .ToList();
        }

        public IEnumerable<Patient> GetPatientsByCity(int cityId)
        {
            return _dbSet.Include(p => p.City)
                        .Where(p => p.CityId == cityId && p.IsActive)
                        .OrderBy(p => p.FirstName)
                        .ThenBy(p => p.LastName)
                        .ToList();
        }

        public IEnumerable<Patient> GetPatientsByDistrict(int districtId)
        {
            return _dbSet.Include(p => p.District)
                        .Include(p => p.City)
                        .Where(p => p.DistrictId == districtId && p.IsActive)
                        .OrderBy(p => p.FirstName)
                        .ThenBy(p => p.LastName)
                        .ToList();
        }

        public IEnumerable<Patient> GetPatientsByAgeRange(int minAge, int maxAge)
        {
            var today = DateTime.Today;
            var maxBirthDate = today.AddYears(-minAge);
            var minBirthDate = today.AddYears(-maxAge - 1);

            return _dbSet.Where(p => p.IsActive &&
                                    p.BirthDate <= maxBirthDate &&
                                    p.BirthDate > minBirthDate)
                        .OrderBy(p => p.BirthDate)
                        .ToList();
        }

        public IEnumerable<Patient> GetPatientsByGender(Gender gender)
        {
            return _dbSet.Where(p => p.Gender == gender && p.IsActive)
                        .OrderBy(p => p.FirstName)
                        .ThenBy(p => p.LastName)
                        .ToList();
        }
    }
}
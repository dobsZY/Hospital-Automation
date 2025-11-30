using HospitalAutomation.Models;
using HospitalAutomation.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HospitalAutomation.Data.Repositories
{
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        public DistrictRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<District> GetDistrictsByCity(int cityId)
        {
            return _context.Districts.Where(d => d.CityId == cityId && d.IsActive).OrderBy(d => d.Name).ToList();
        }

        public IEnumerable<District> GetDistrictsWithCity()
        {
            return _context.Districts.Include(d => d.City).Where(d => d.IsActive).OrderBy(d => d.Name).ToList();
        }
    }
}


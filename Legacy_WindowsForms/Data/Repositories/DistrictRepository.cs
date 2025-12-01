using HospitalAutomation.Models;
using HospitalAutomation.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace HospitalAutomation.Data.Repositories
{
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        public DistrictRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<District> GetDistrictsByCity(int cityId)
        {
            return Context.Districts.Where(d => d.CityId == cityId && d.IsActive).OrderBy(d => d.Name).ToList();
        }

        public IEnumerable<District> GetDistrictsWithCity()
        {
            return Context.Districts.Include("City").Where(d => d.IsActive).OrderBy(d => d.Name).ToList();
        }

        private HospitalDbContext Context => (HospitalDbContext)_context;
    }
}
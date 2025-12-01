using HospitalAutomation.Models;
using HospitalAutomation.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace HospitalAutomation.Data.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<City> GetCitiesWithDistricts()
        {
            return Context.Cities.Include("Districts").Where(c => c.IsActive).OrderBy(c => c.Name).ToList();
        }

        public City GetCityWithDistricts(int cityId)
        {
            return Context.Cities.Include("Districts").FirstOrDefault(c => c.Id == cityId && c.IsActive);
        }

        public IEnumerable<City> GetCitiesByRegion(string region)
        {
            return Context.Cities.Where(c => c.Region == region && c.IsActive).OrderBy(c => c.Name).ToList();
        }

        private HospitalDbContext Context => (HospitalDbContext)_context;
    }
}
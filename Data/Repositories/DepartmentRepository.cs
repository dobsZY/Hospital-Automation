using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;

namespace HospitalAutomation.Data.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(HospitalDbContext context) : base(context)
        {
        }

        public Department GetByCode(string code)
        {
            return _dbSet.FirstOrDefault(d => d.Code == code && d.IsActive);
        }

        public IEnumerable<Department> GetActiveDepartments()
        {
            return _dbSet.Where(d => d.IsActive).OrderBy(d => d.Name).ToList();
        }

        public Department GetDepartmentWithDoctors(int departmentId)
        {
            return _dbSet.Include("Doctors")
                        .FirstOrDefault(d => d.Id == departmentId && d.IsActive);
        }

        public IEnumerable<Department> GetDepartmentsWithDoctorCount()
        {
            return _dbSet.Where(d => d.IsActive)
                        .Select(d => new Department
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Code = d.Code,
                            Description = d.Description,
                            IsActive = d.IsActive,
                            CreatedDate = d.CreatedDate,
                            UpdatedDate = d.UpdatedDate
                        })
                        .OrderBy(d => d.Name)
                        .ToList();
        }

        public bool IsCodeExists(string code)
        {
            return _dbSet.Any(d => d.Code == code && d.IsActive);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(HospitalDbContext context) : base(context)
        {
        }

        public User GetByUsername(string username)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username && u.IsActive);
        }

        public User GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(u => u.Email == email && u.IsActive);
        }

        public IEnumerable<User> GetByRole(UserRole role)
        {
            return _dbSet.Where(u => u.Role == role && u.IsActive)
                        .OrderBy(u => u.FirstName)
                        .ThenBy(u => u.LastName)
                        .ToList();
        }

        public IEnumerable<User> GetDoctors()
        {
            return _dbSet.Include(u => u.Department)
                        .Where(u => u.Role == UserRole.Doctor && u.IsActive)
                        .OrderBy(u => u.FirstName)
                        .ThenBy(u => u.LastName)
                        .ToList();
        }

        public IEnumerable<User> GetDoctorsByDepartment(int departmentId)
        {
            return _dbSet.Include(u => u.Department)
                        .Where(u => u.Role == UserRole.Doctor && 
                                   u.DepartmentId == departmentId && u.IsActive)
                        .OrderBy(u => u.FirstName)
                        .ThenBy(u => u.LastName)
                        .ToList();
        }

        public bool IsUsernameExists(string username)
        {
            return _dbSet.Any(u => u.Username == username && u.IsActive);
        }

        public bool IsEmailExists(string email)
        {
            return _dbSet.Any(u => u.Email == email && u.IsActive);
        }

        public User Authenticate(string username, string password)
        {
            var user = GetByUsername(username);
            if (user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }
    }
}
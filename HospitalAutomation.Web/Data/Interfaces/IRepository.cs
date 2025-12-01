using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HospitalAutomation.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Basic CRUD operations
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        
        T SingleOrDefault(Expression<Func<T, bool>> expression);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression);
        
        void Add(T entity);
        Task AddAsync(T entity);
        
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        
        void Update(T entity);
        
        // Pagination
        IEnumerable<T> GetPaged(int page, int pageSize);
        int Count();
        int Count(Expression<Func<T, bool>> expression);
    }
}


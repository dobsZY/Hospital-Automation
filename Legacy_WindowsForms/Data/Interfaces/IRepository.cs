using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HospitalAutomation.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Basic CRUD operations
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        T SingleOrDefault(Expression<Func<T, bool>> expression);
        
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        
        void Update(T entity);
        
        // Pagination
        IEnumerable<T> GetPaged(int page, int pageSize);
        int Count();
        int Count(Expression<Func<T, bool>> expression);
    }
}
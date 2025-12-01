using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;

namespace HospitalAutomation.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly HospitalDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(HospitalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.Where(x => x.IsActive).ToList();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(x => x.IsActive).ToList();
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(x => x.IsActive).SingleOrDefault();
        }

        public virtual void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.CreatedDate = DateTime.Now;
            entity.IsActive = true;
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
                entity.IsActive = true;
            }
            _dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.UpdatedDate = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.IsActive = false;
            entity.UpdatedDate = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                entity.IsActive = false;
                entity.UpdatedDate = DateTime.Now;
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void HardDelete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
        }

        public virtual IEnumerable<T> GetPaged(int page, int pageSize)
        {
            return _dbSet.Where(x => x.IsActive)
                         .OrderBy(x => x.Id)
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();
        }

        public virtual int Count()
        {
            return _dbSet.Count(x => x.IsActive);
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).Count(x => x.IsActive);
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).Any(x => x.IsActive);
        }
    }
}
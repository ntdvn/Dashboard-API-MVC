using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DashboardMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }
        protected ApplicationDbContext DbContext
        {
            get { return _dbContext ?? (_dbContext = DbFactory.Init()); }
        }
        #endregion


        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        #region Implementation
        public T Add(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Count<T>(predicate) > 0;
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return _dbSet.Count(where);
        }

        public T Delete(T entity)
        {
            return _dbSet.Remove(entity).Entity;
        }

        public T Delete(int id)
        {
            var entity = _dbSet.Find(id);
            return _dbSet.Remove(entity).Entity;
        }

        public void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return _dbContext.Set<T>().AsQueryable();
        }

        public IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return _dbContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? _dbContext.Set<T>().Where<T>(predicate).AsQueryable() : _dbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return _dbContext.Set<T>().FirstOrDefault(expression);
        }

        public T GetSingleById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
    #endregion
}
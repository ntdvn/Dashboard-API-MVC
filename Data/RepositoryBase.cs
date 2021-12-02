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


        public RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }



        #region Implementation
        public T Add(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
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

        public void DeletesBy(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find();
        }

        public T GetBy(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Find(expression);
        }

        public IQueryable<T> Gets()
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> GetsBy(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where<T>(expression).AsQueryable<T>();
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return _dbSet.Count(where);
        }

        public bool CheckContains(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Count<T>(expression) > 0;
        }
        #endregion
    }
}
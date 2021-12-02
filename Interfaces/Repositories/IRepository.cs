using System.Linq;
using System.Collections;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace DashboardMVC.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        void Update(T entity);

        T Delete(T entity);

        T Delete(int id);

        void DeletesBy(Expression<Func<T, bool>> expression);

        T GetById(int id);

        T GetBy(Expression<Func<T, bool>> expression);

        IQueryable<T> Gets();

        IQueryable<T> GetsBy(Expression<Func<T, bool>> expression);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> expression);

    }
}
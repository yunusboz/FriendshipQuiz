﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(bool trackChanges, string includeProperties = "");
        T Get(Expression<Func<T, bool>> predicate,bool trackChanges ,string includeProperties = "");
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Data.Infrastructure
{
    public interface IRepository<T> where T: class
    {
        //marks an entity as new
        void Add(T entity);
        //marks an entity as modified
        void Update(T entity);
        //marks an entity to be removed
        void Delete(T entity);
        //delete multi records
        void DeleteMulti(Expression<Func<T, bool>> where);
        //GEt an entity by int id
        T GetSingleById(int id);
        
        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);


        IQueryable<T> GetAll(string[] includes = null);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string[] includes = null);

        IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}

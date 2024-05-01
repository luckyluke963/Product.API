﻿using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Interface
{
    public interface IGenericRepository <T> where T : BasicEntity<int>
    { 
        Task<IReadOnlyList<T>> GetAllAsync();

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);

        Task<T> GetAsync(int id);   

        Task<T> GetByIdAsync(int id,params Expression<Func<T, object>>[] includes);

        Task<T> GetAsync(T id);

        Task AddAsync(T entity);

        Task UpdateAsync(int id,T entity);  

        Task DeleteAsync(int id);
    }
}

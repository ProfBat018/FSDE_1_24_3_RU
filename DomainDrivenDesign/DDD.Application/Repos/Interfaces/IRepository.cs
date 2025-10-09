using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Application.Services.Interfaces;

public interface IRepository<T> where T : class
{
    Task<TResult?> GetFirstOrDefault<TResult>(
       Expression<Func<T, bool>> filter,
       Expression<Func<T, TResult>>? selectFilter = null,
       string? includeProperties = null,
       bool tracked = true);

    Task<IEnumerable<TResult>> GetAll<TResult>(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, TResult>>? selectFilter = null,
        string? includeProperties = null);
    Task AddAsync(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);
}
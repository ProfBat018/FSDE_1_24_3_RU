using DDD.Application.Repos.Interfaces;
using DDD.Application.Services.Interfaces;
using DDD.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly EcommerceDbContext _context;
    private protected DbSet<T> dbSet;

    public Repository(EcommerceDbContext context)
    {
        _context = context;
        dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async Task<IEnumerable<TResult>> GetAll<TResult>(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, TResult>>? selectFilter = null,
        string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            var includes = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var include in includes)
                query = query.Include(include.Trim());
        }

        if (selectFilter != null)
            return await query.Select(selectFilter).ToListAsync();

        return await query.Cast<TResult>().ToListAsync();
    }

    public async Task<TResult?> GetFirstOrDefault<TResult>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, TResult>>? selectFilter = null,
        string? includeProperties = null,
        bool tracked = true)
    {
        IQueryable<T> query = dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            var includes = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var include in includes)
                query = query.Include(include.Trim());
        }

        if (selectFilter != null)
            return await query.Where(filter).Select(selectFilter).FirstOrDefaultAsync();

        return (TResult?)(object?)await query.FirstOrDefaultAsync(filter);
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}

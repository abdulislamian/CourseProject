using Courseproject.Common.Model;
using CourseProject.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Infrastructure;


public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext dbContext;
    private DbSet<T> dbSet;
    public GenericRepository(ApplicationDbContext _DbContext)
    {
        dbContext = _DbContext;
        dbSet = dbContext.Set<T>();
    }
    public void Delete(T entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        dbSet.Attach(entity);
        dbSet.Remove(entity);
    }

    public async Task<List<T>> GetAsync(int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        foreach(var include in includes)
            query=query.Include(include);

        if (skip != null)
            query = query.Skip(skip.Value);

        if(take != null)
            query = query.Take(take.Value);

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id,params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(entity => entity.Id == id);

        foreach(var include in includes)
            query=query.Include(include);

        return await query.SingleOrDefaultAsync();
    }

    public async Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>>[] filters, int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        
        foreach(var filter in filters)
            query =query.Where(filter);

        foreach(var include in includes)
            query=query.Include(include);

        if(skip != null)
            query = query.Skip(skip.Value);

        if(take !=null)
            query = query.Take(take.Value);

        return await query.ToListAsync();
    }

    public async Task<int> InsertAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        return entity.Id;
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async void Update(T entity)
    {
        dbSet.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;
    }
}

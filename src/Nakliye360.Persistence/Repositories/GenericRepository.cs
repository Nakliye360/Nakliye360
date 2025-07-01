using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Nakliye360.Application.Repositories;
using Nakliye360.Domain.Entities;
using Nakliye360.Persistence.Contexts;
using System.Linq.Expressions;

namespace Nakliye360.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{

    readonly private Nakliye360DbContext _context;
    public GenericRepository(Nakliye360DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        if (!tracking)
            return Table.AsNoTracking();
        return Table.AsQueryable();

    }

    public async Task<T> GetByIdAsync(int id, bool tracking = true)
    {
        var query = tracking ? Table.AsQueryable() : Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        var query = tracking ? Table.AsQueryable() : Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(filter);
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        var query = tracking ? Table.Where(filter).AsQueryable() : Table.Where(filter).AsNoTracking();
        return query;
    }

    public async Task<T> AddAsync(T data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        EntityEntry<T> entityEntry = await Table.AddAsync(data);
        return entityEntry.Entity;
    }

    public async Task<List<T>> AddRangeAsync(List<T> datas)
    {
        await Table.AddRangeAsync(datas);
        return datas;
    }

    public bool Delete(T? data, int? id)
    {
        if(id != null)
            data = Table.FirstOrDefault(x => x.Id == id);

        Table.Remove(data);
        return true;
    }

    public bool DeleteRange(List<T> data)
    {
        Table.RemoveRange(data);
        return true;
    }

    public bool Update(T data)
    {
        EntityEntry entityEntry = Table.Update(data);
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}

using Microsoft.EntityFrameworkCore;
using Nakliye360.Domain.Entities;
using System.Linq.Expressions;

namespace Nakliye360.Application.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> filter, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true);
        Task<T> GetByIdAsync(int id, bool tracking = true);

        Task<T> AddAsync(T data);
        Task<List<T>> AddRangeAsync(List<T> datas);
        bool Update(T data);
        bool Delete(T? data, int? id);
        bool DeleteRange(List<T> data);
        Task<int> SaveAsync();
    }
}

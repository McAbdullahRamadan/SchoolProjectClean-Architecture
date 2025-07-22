using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.InfastructureBases
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task DeleteRangeAsnc(ICollection<T> entities);
        Task<T> GetByIdAsync(int id);
        Task SaveChangesAsync();
        void Commit();
        void RollBack();
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableASTracking();
        IDbContextTransaction BeginTransaction();
        Task<T> AddAsync(T entites);
        Task AddRangeAsync(ICollection<T> entities);

        Task UpdateAsync(T entites);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entites);



    }
}

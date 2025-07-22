using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.InfastructureBases
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        public readonly ApplicationDbContext _dbContext;
        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<T> AddAsync(T entites)
        {
            await _dbContext.Set<T>().AddAsync(entites);
            await _dbContext.SaveChangesAsync();
            return entites;
        }

        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync();
            await _dbContext.SaveChangesAsync();

        }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();
        }

        public virtual async Task DeleteAsync(T entites)
        {
            _dbContext.Set<T>().Remove(entites);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsnc(ICollection<T> entities)
        {
            foreach (var etitey in entities)
            {
                _dbContext.Entry(etitey).State = EntityState.Deleted;

            }
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetTableASTracking()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return _dbContext.Set<T>().AsNoTracking().AsQueryable();
        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public virtual async Task UpdateAsync(T entitiy)
        {
            _dbContext.Set<T>().Update(entitiy);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(ICollection<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();

        }
    }
}

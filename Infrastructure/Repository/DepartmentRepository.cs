using Data.Entites;
using Infrastructure.Abstract;
using Infrastructure.DataContext;
using Infrastructure.InfastructureBases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        #region Felids
        public DbSet<Department> departments;
        #endregion
        #region Construtors
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            departments = dbContext.Set<Department>();


        }

        #endregion
        #region Handel Function
        #endregion

    }
}

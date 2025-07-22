using Data.Entites;
using Infrastructure.AbstractRepository;
using Infrastructure.DataContext;
using Infrastructure.InfastructureBases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class InstrctorRepository : GenericRepositoryAsync<Instructor>, IInstrctorRepository
    {

        #region Felids
        public DbSet<Instructor> instructors;
        #endregion
        #region Construtors
        public InstrctorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            instructors = dbContext.Set<Instructor>();


        }

        #endregion
        #region Handel Function
        #endregion
    }
}

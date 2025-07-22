using Data.Entites;
using Infrastructure.AbstractRepository;
using Infrastructure.DataContext;
using Infrastructure.InfastructureBases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class SubjectRepository : GenericRepositoryAsync<Subject>, ISubjectRepository
    {
        #region Feldis
        public DbSet<Subject> subjects;
        #endregion
        #region Constructors
        public SubjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            subjects = dbContext.Set<Subject>();


        }
        #endregion
        #region Handel Function
        #endregion

    }
}

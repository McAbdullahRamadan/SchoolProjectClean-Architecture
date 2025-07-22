using Data.Entites;
using Infrastructure.Abstract;
using Infrastructure.DataContext;
using Infrastructure.InfastructureBases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        #region Fields
        public readonly DbSet<Student> _students;
        #endregion
        #region Constructors

        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _students = dbContext.Set<Student>();

        }


        #endregion
        #region Handels Function
        public async Task<List<Student>> GetStudentAsync()
        {
            return await _students.Include(x => x.Departments).ToListAsync();
        }
        #endregion


    }
}

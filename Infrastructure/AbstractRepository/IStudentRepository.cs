using Data.Entites;
using Infrastructure.InfastructureBases;

namespace Infrastructure.Abstract
{
    public interface IStudentRepository :IGenericRepositoryAsync<Student>
    {
        public Task<List<Student>> GetStudentAsync();
    }
}

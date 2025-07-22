using Data.Entites;

namespace Service.Abstruct
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentById(int id);
    }
}

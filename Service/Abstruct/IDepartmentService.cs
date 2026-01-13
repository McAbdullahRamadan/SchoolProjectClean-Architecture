using Data.Entites;

namespace Service.Abstruct
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentById(int id);
        public Task<bool> IsDEpartmentIdExist(int Department);
    }
}

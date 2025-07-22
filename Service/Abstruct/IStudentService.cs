using Data.Entites;
using Data.Helpers;

namespace Service.Abstruct
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsAsync();
        public Task<Student> GetStudentsByidAsyncEncloud(int id);
        public Task<Student> GetStudentByid(int id);
        public Task<string> AddAsync(Student student);
        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExistExcloudSelf(string name, int id);
        public Task<string> EditAsync(Student student);
        public Task<string> DeleteAsync(Student student);
        public IQueryable<Student> GetStudentQuerable();
        public IQueryable<Student> GetStudentByDepartmentbyIDQuerable(int DID);

        public IQueryable<Student> FilterStudentPagintedQuerable(StudentOredringEnum Oederenum, string search);




    }
}

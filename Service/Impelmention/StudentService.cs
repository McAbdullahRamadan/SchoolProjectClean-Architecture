using Data.Entites;
using Data.Helpers;
using Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;
using Service.Abstruct;

namespace Service.Impelmention
{
    public class StudentService : IStudentService
    {
        #region Fildes
        public readonly IStudentRepository _studentRepository;
        #endregion
        #region Constructors
        public StudentService(IStudentRepository studentRepository)
        {

            _studentRepository = studentRepository;

        }


        #endregion
        #region Handel Function
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _studentRepository.GetStudentAsync();
        }

        public async Task<Student> GetStudentsByidAsyncEncloud(int id)
        {
            var student = _studentRepository.GetTableNoTracking()
                                               .Include(x => x.Departments)
                                               .Where(x => x.StudID.Equals(id))
                                               .FirstOrDefault();
            return student;
        }
        public async Task<string> AddAsync(Student student)
        {
            //check if the Name is Exist Or No

            await _studentRepository.AddAsync(student);
            return "Success";
        }
        public async Task<bool> IsNameExist(string name)
        {
            var isExist = await _studentRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(name)).FirstOrDefaultAsync();
            if (isExist == null)
                return false;
            return true;

        }

        public async Task<bool> IsNameExistExcloudSelf(string name, int id)
        {
            var isExist = await _studentRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(name) & !x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (isExist == null)
                return false;
            return true;
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var Trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await Trans.CommitAsync();
                return "Success";

            }
            catch
            {
                await Trans.RollbackAsync();
                return "Failed";
            }

        }

        public async Task<Student> GetStudentByid(int id)
        {
            var result = await _studentRepository.GetByIdAsync(id);
            return result;
        }

        public IQueryable<Student> GetStudentQuerable()
        {
            return _studentRepository.GetTableNoTracking().Include(x => x.Departments).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPagintedQuerable(StudentOredringEnum OrderEnum, string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(x => x.Departments).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.NameAr.Contains(search) || x.Adreess.Contains(search));

            }
            switch (OrderEnum)
            {
                case StudentOredringEnum.StudID:
                    querable = querable.OrderBy(x => x.StudID);
                    break;
                case StudentOredringEnum.Name:
                    querable = querable.OrderBy(x => x.NameAr);
                    break;
                case StudentOredringEnum.Address:
                    querable = querable.OrderBy(x => x.Adreess);
                    break;
                case StudentOredringEnum.Department:
                    querable = querable.OrderBy(x => x.Departments.DNameAr);
                    break;

            }


            return querable;
        }

        public IQueryable<Student> GetStudentByDepartmentbyIDQuerable(int DID)
        {
            return _studentRepository.GetTableNoTracking().Where(x => x.DID.Equals(DID)).AsQueryable();
        }
        #endregion






















    }
}

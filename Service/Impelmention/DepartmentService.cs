using Data.Entites;
using Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;
using Service.Abstruct;

namespace Service.Impelmention
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        public readonly IDepartmentRepository _departmentRepository;
        #endregion
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;

        }


        #endregion
        #region Handle
        public async Task<Department> GetDepartmentById(int id)
        {
            var resultStudent = await _departmentRepository.GetTableNoTracking().Where(x => x.DID.Equals(id))
                                                         .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subjects)

                                                         .Include(x => x.Instructors)
                                                         .Include(x => x.Instructor).FirstOrDefaultAsync();
            return resultStudent;

        }
        #endregion
    }
}

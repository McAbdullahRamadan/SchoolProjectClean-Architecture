using AutoMapper;

namespace Core.Features.Mapping.DepartmentMapp.QueryMapping
{
    public partial class DepartmentProfil : Profile
    {
        public DepartmentProfil()
        {
            GetDepartmentByIDMapping();

        }
    }
}

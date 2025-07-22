using Core.Features.Students.Queries.Results;
using Data.Entites;

namespace Core.Features.Mapping.CommandMapp
{
    public partial class StudMapping
    {
        public void GetStudentByIdMapp()
        {
            CreateMap<Student, GetSingleStudentResponse>()
                .ForMember(dst => dst.DepartmentName, opt => opt.MapFrom(src => src.Departments.DNameAr))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Localized(src.NameAr, src.NameEn)));
            ;


        }
    }
}

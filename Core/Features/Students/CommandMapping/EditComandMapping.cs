using Core.Features.Students.Commands.Models;
using Data.Entites;

namespace Core.Features.Mapping.CommandMapp
{
    public partial class StudMapping
    {
        public void EditComandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
                .ForMember(dst => dst.DID, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dst => dst.NameAr, opt => opt.MapFrom(src => src.NameAR))
                .ForMember(dst => dst.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dst => dst.StudID, opt => opt.MapFrom(src => src.Id));



        }
    }
}

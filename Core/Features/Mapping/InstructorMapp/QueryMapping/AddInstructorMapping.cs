using Core.Features.Instractor.Command.Model;
using Data.Entites;

namespace Core.Features.Mapping.InstructorMapp.QueryMapping
{
    public partial class InstrcutorProfil
    {
        public void AddInstructorMapping()
        {

            CreateMap<AddInstractorCommand, Instructor>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.ENameAr, opt => opt.MapFrom(src => src.ENameAr))
                .ForMember(dest => dest.ENameEn, opt => opt.MapFrom(src => src.ENameEn));



        }
    }
}

using AutoMapper;

namespace Core.Features.Mapping.CommandMapp
{
    public partial class StudMapping : Profile
    {
        public StudMapping()
        {
            GetStudentMapp();
            GetStudentByIdMapp();
            AddComandMapping();
            EditComandMapping();


        }
    }
}

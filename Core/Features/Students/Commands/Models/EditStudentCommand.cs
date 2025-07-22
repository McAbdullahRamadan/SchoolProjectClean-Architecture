using Core.Besec;
using MediatR;

namespace Core.Features.Students.Commands.Models
{
    public class EditStudentCommand : IRequest<Response<string>>
    {

        public int Id { get; set; }

        public string NameAR { get; set; }
        public string NameEn { get; set; }


        public string Adreess { get; set; }

        public string? Phone { get; set; }

        public int DepartmentId { get; set; }

    }
}

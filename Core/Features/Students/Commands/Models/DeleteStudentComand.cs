using Core.Besec;
using MediatR;

namespace Core.Features.Students.Commands.Models
{
    public class DeleteStudentComand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteStudentComand(int id)
        {
            Id = id;

        }
    }
}

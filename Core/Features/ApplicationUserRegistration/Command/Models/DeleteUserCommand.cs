using Core.Besec;
using MediatR;

namespace Core.Features.UserRegistration.Command.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteUserCommand(int id)
        {
            Id = id;

        }
    }
}

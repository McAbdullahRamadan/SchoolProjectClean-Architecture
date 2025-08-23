using Core.Besec;
using MediatR;

namespace Core.Features.Authentication.Command.Models
{
    public class SendResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}

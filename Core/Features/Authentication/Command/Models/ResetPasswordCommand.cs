using Core.Besec;
using MediatR;

namespace Core.Features.Authentication.Command.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

    }
}

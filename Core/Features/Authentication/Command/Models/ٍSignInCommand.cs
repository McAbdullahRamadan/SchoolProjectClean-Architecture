using Core.Besec;
using MediatR;

namespace Core.Features.Authentication.Command.Models
{
    public class SignInCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}

using Core.Besec;
using Data.Helpers;
using MediatR;

namespace Core.Features.Authentication.Command.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}

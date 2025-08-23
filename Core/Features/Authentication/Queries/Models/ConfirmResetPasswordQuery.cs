using Core.Besec;
using MediatR;

namespace Core.Features.Authentication.Queries.Models
{
    public class ConfirmResetPasswordQuery : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }


    }
}

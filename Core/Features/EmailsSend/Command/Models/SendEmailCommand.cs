using Core.Besec;
using MediatR;

namespace Core.Features.EmailsSend.Command.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Message { get; set; }

    }
}

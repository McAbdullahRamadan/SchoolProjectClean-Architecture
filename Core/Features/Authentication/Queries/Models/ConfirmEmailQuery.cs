using Core.Besec;
using MediatR;

namespace Core.Features.Authentication.Queries.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public int userId { get; set; }
        public string code { get; set; }

    }
}

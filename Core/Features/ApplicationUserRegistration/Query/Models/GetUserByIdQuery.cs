using Core.Besec;
using Core.Features.UserRegistration.Query.Results;
using MediatR;

namespace Core.Features.UserRegistration.Query.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;

        }
    }
}

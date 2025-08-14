using Core.Besec;
using Core.Features.Authorization.Queries.Result;
using MediatR;

namespace Core.Features.Authorization.Queries.Models
{
    public class GetRolesListQuery : IRequest<Response<List<GetRolesListResult>>>
    {
    }
}

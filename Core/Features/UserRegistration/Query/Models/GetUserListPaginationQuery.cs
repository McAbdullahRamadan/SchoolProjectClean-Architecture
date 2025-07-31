using Core.Features.UserRegistration.Query.Results;
using Core.Wrappers;
using MediatR;

namespace Core.Features.UserRegistration.Query.Models
{
    public class GetUserListPaginationQuery : IRequest<PaginatedResult<GetUserListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}

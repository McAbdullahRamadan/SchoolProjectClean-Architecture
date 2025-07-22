using Core.Features.Students.Queries.Results;
using Core.Wrappers;
using Data.Helpers;
using MediatR;

namespace Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedListQuery : IRequest<PaginatedResult<GetStudentpaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOredringEnum OrderBy { get; set; }
        public string? Search { get; set; }

    }
}

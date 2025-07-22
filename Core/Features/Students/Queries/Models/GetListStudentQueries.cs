using Core.Besec;
using Core.Features.Students.Queries.Results;
using MediatR;

namespace Core.Features.Students.Queries.Handlers
{
    public class GetListStudentQueries : IRequest<Response<List<GetSudentListResult>>>
    {
    }
}

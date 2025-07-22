using Core.Besec;
using Core.Features.Department.Queries.Result;
using MediatR;

namespace Core.Features.Department.Queries.Model
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentByIdResponse>>
    {
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }
        public int Id { get; set; }

    }
}

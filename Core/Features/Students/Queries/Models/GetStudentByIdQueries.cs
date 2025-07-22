using Core.Besec;
using Core.Features.Students.Queries.Results;
using MediatR;

namespace Core.Features.Students.Queries.Models
{
    public class GetStudentByIdQueries : IRequest<Response<GetSingleStudentResponse>>
    {
        public int Id { get; set; }
        public GetStudentByIdQueries(int id)
        {
            Id = id;

        }
    }
}

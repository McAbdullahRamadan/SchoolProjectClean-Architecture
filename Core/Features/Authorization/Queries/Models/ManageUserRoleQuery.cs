using Core.Besec;
using Data.DTORequset;
using MediatR;

namespace Core.Features.Authorization.Queries.Models
{
    public class ManageUserRoleQuery : IRequest<Response<ManageUserRoleResult>>
    {
        public int UserId { get; set; }
    }
}

using Core.Besec;
using Data.DTORequset;
using MediatR;

namespace Core.Features.Authorization.Command.Models
{
    public class UpdateRoleUserCommand : UpdateUserRoleRequest, IRequest<Response<string>>
    {
    }
}

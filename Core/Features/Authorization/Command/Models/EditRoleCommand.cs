using Core.Besec;
using Data.DTORequset;
using MediatR;

namespace Core.Features.Authorization.Command.Models
{
    public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
    {


    }
}

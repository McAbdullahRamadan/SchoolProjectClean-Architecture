using Core.Besec;
using MediatR;

namespace Core.Features.Authorization.Command.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}

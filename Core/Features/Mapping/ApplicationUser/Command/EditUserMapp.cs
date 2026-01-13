using Core.Features.UserRegistration.Command.Models;
using Data.Entites.Identity;

namespace Core.Features.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfil
    {
        public void EditUserMapp()
        {
            CreateMap<EditUserCommand, UserIdentity>();

        }
    }
}

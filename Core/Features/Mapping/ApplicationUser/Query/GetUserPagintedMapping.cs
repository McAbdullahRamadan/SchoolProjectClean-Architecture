using Core.Features.UserRegistration.Query.Results;
using Data.Entites.Identity;

namespace Core.Features.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfil
    {
        public void GetUserPagintedMapping()
        {
            CreateMap<UserIdentity, GetUserListResponse>();
        }
    }
}

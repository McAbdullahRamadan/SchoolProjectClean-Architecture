using AutoMapper;

namespace Core.Features.Mapping.ApplicationUser
{

    public partial class ApplicationUserProfil : Profile
    {
        public ApplicationUserProfil()
        {
            AddUserMapping();
            GetUserPagintedMapping();

        }
    }

}

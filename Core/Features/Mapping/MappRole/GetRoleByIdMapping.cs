using Core.Features.Authorization.Queries.Result;
using Data.Entites.Identity;

namespace Core.Features.Mapping.MappRole
{
    public partial class RoleProfile
    {
        public void GetRoleByIdMapping()
        {
            CreateMap<RoleSys, GetRoleByIdResult>();


        }
    }
}

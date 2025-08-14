using AutoMapper;

namespace Core.Features.Mapping.MappRole
{
    public partial class RoleProfile : Profile
    {
        public RoleProfile()
        {
            GetRoleListMapping();
            GetRoleByIdMapping();
        }
    }
}

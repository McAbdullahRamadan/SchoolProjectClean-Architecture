using AutoMapper;
using Core.Besec;
using Core.Features.Authorization.Queries.Models;
using Core.Features.Authorization.Queries.Result;
using Core.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authorization.Queries.Handle
{
    public class RoleQueryHandler : ResponseHadlar,
        IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>


    {
        private readonly IStringLocalizer<SheardResource> _localizer;
        private readonly IAuthorizService _authorizService;
        private readonly IMapper _mapper;


        public RoleQueryHandler(IStringLocalizer<SheardResource> localizer, IMapper mapper, IAuthorizService authorizService) : base(localizer)
        {
            _localizer = localizer;
            _authorizService = authorizService;
            _mapper = mapper;


        }

        public async Task<Response<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizService.GetRoleAsync();

            var result = _mapper.Map<List<GetRolesListResult>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizService.GetRoleByIdAsync(request.Id);
            if (role == null)
                return NotFound<GetRoleByIdResult>(_localizer[KeySharedResource.RoleNotExist]);
            var result = _mapper.Map<GetRoleByIdResult>(role);
            return Success(result);
        }
    }
}

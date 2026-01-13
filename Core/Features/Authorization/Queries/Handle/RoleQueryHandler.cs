using AutoMapper;
using Core.Besec;
using Core.Features.Authorization.Queries.Models;
using Core.Features.Authorization.Queries.Result;
using Core.Resource;
using Data.DTORequset;
using Data.Entites.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authorization.Queries.Handle
{
    public class RoleQueryHandler : ResponseHandlar,
        IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>,
        IRequestHandler<ManageUserRoleQuery, Response<ManageUserRoleResult>>



    {
        private readonly IStringLocalizer<SheardResource> _localizer;
        private readonly IAuthorizService _authorizService;
        private readonly IMapper _mapper;
        private readonly UserManager<UserIdentity> _userManager;



        public RoleQueryHandler(IStringLocalizer<SheardResource> localizer, IMapper mapper, IAuthorizService authorizService, UserManager<UserIdentity> userManager) : base(localizer)
        {
            _localizer = localizer;
            _authorizService = authorizService;
            _mapper = mapper;
            _userManager = userManager;

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

        public async Task<Response<ManageUserRoleResult>> Handle(ManageUserRoleQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return NotFound<ManageUserRoleResult>(_localizer[KeySharedResource.userIsNotFound]);
            var result = await _authorizService.GetManageRoleUser(user);
            return Success(result);

        }
    }
}

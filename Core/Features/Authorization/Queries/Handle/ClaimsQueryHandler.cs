using Core.Besec;
using Core.Features.Authorization.Queries.Models;
using Core.Resource;
using Data.Entites.Identity;
using Data.Helpers.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authorization.Queries.Handle
{
    public class ClaimsQueryHandler : ResponseHandlar,
        IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResult>>
    {
        #region Fialds
        private readonly IStringLocalizer<SheardResource> _stringLocalizer;
        private readonly IAuthorizService _authorizService;
        private readonly UserManager<UserIdentity> _userManager;



        #endregion
        #region Constructor
        public ClaimsQueryHandler(IStringLocalizer<SheardResource> stringLocalizer,
            IAuthorizService authorizService, UserManager<UserIdentity> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizService = authorizService;
            _userManager = userManager;
        }


        #endregion
        #region Handle Query
        public async Task<Response<ManageUserClaimsResult>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserClaimsResult>(_stringLocalizer[KeySharedResource.NoFound]);
            var result = await _authorizService.GetManageUserClaimsData(user);
            return Success(result);
        }
        #endregion

    }
}

using Core.Besec;
using Core.Features.Authorization.Command.Models;
using Core.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authorization.Command.Handle
{
    public class UserClaimsHandler : ResponseHandlar,
        IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fialdes
        private readonly IStringLocalizer<SheardResource> _localizer;
        private readonly IAuthorizService _authorizService;

        #endregion
        #region Constructors
        public UserClaimsHandler(IStringLocalizer<SheardResource> stringLocalizer, IAuthorizService authorizService) : base(stringLocalizer)
        {
            _localizer = stringLocalizer;
            _authorizService = authorizService;

        }
        #endregion
        #region Handle function
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizService.UpdateUserClaims(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_localizer[KeySharedResource.userIsNotFound]);
                case "FailedToRemoveOldClaims": return BadRequst<string>(_localizer[KeySharedResource.FailedToRemoveOldClaims]);
                case "FailedToAddNewClaims": return BadRequst<string>(_localizer[KeySharedResource.FailedToAddNewClaims]);

                case "FailedToUpdateClaims": return BadRequst<string>(_localizer[KeySharedResource.FailedToUpdateClaims]);
            }
            return Success<string>(_localizer[KeySharedResource.Success]);
        }
        #endregion


    }
}

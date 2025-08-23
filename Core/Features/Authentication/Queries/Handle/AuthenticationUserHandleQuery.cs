using Core.Besec;
using Core.Features.Authentication.Queries.Models;
using Core.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authentication.Queries.Handle
{
    public class AuthenticationUserHandleQuery : ResponseHadlar,

        IRequestHandler<AuthorizeUserQuery, Response<string>>,
        IRequestHandler<ConfirmEmailQuery, Response<string>>


    {
        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;

        private readonly IAuthenticationService _authenticationService;





        #endregion
        #region Constructors
        public AuthenticationUserHandleQuery(IStringLocalizer<SheardResource> stringLocalizer
           , IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _Localizer = stringLocalizer;


            _authenticationService = authenticationService;
        }
        #endregion
        #region Handle Funcation


        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
            {
                return Success(result);
            }
            return Unauthorized<string>(_Localizer[KeySharedResource.TokenIsExpired]);

        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _authenticationService.ConfirmEmail(request.userId, request.code);
            if (confirmEmail == "ErrorWhenConfirmEmail")
                return BadRequst<string>(_Localizer[KeySharedResource.ErrorWhenConfirmEmail]);
            return Success<string>(_Localizer[KeySharedResource.ConfirmEmailIsDone]);
        }
        #endregion
    }
}
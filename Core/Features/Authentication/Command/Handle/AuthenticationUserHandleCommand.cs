using AutoMapper;
using Core.Besec;
using Core.Features.Authentication.Command.Models;
using Core.Resource;
using Data.Entites.Identity;
using Data.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authentication.Command.Handle
{
    public class AuthenticationUserHandleCommand : ResponseHadlar,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>,
        IRequestHandler<SendResetPasswordCommand, Response<string>>


    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SheardResource> _Localizer;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly IAuthenticationService _authenticationService;





        #endregion
        #region Constructors
        public AuthenticationUserHandleCommand(IStringLocalizer<SheardResource> stringLocalizer, IMapper mapper
            , UserManager<UserIdentity> userManage, SignInManager<UserIdentity> signInManager,
            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _Localizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManage;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion
        #region Handle Funcation
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //check if User is Exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);
            //return the userName Not Found
            if (user == null)
                return BadRequst<JwtAuthResult>(_Localizer[KeySharedResource.UserNameIsinvalid]);

            var singInUser = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //confirm email
            if (!user.EmailConfirmed)
                return BadRequst<JwtAuthResult>(_Localizer[KeySharedResource.EmailNotConfirmed]);
            //if Failed return passeord is worng
            if (!singInUser.IsCompletedSuccessfully)
                return BadRequst<JwtAuthResult>(_Localizer[KeySharedResource.PasswordIsNotCorrect]);
            //genrate Token
            var accessToken = await _authenticationService.GetJWTToken(user);
            //return Token
            return Success(accessToken);



        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var JwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
            var userid = await _authenticationService.VAlidatDetalis(JwtToken, request.AccessToken, request.RefreshToken);
            switch (userid)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(_Localizer[KeySharedResource.Algorithm]);
                case ("TokenisNotExpired", null): return Unauthorized<JwtAuthResult>(_Localizer[KeySharedResource.TokenisNotExpired]);
                case ("RefreshTokenisNotFound", null): return Unauthorized<JwtAuthResult>(_Localizer[KeySharedResource.RefreshTokenisNotFound]);
                case ("RefreshTokenisExpired", null): return Unauthorized<JwtAuthResult>(_Localizer[KeySharedResource.RefreshTokenisExpired]);

            }
            var (userId, expirydate) = userid;
            var User = await _userManager.FindByIdAsync(userId);
            if (User == null)
            {
                return NotFound<JwtAuthResult>();

            }
            var results = await _authenticationService.GetRefreshToken(User, JwtToken, expirydate, request.RefreshToken);
            return Success(results);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCode(request.Email);
            switch (result)

            {
                case "UserNotFound": return BadRequst<string>(_Localizer[KeySharedResource.userIsNotFound]);
                case "ErrorInUpdateUser": return BadRequst<string>(_Localizer[KeySharedResource.TryAgainInAnotherTime]);
                case "Failed": return BadRequst<string>(_Localizer[KeySharedResource.TryAgainInAnotherTime]);
                case "Success": return Success<string>("");
                default: return BadRequst<string>(_Localizer[KeySharedResource.TryAgainInAnotherTime]);

            }
        }
        #endregion
    }
}
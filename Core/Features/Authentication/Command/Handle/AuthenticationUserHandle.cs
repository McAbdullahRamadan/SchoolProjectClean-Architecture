using AutoMapper;
using Core.Besec;
using Core.Features.Authentication.Command.Models;
using Core.Resource;
using Data.Entites.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authentication.Command.Handle
{
    public class AuthenticationUserHandle : ResponseHadlar,
        IRequestHandler<SignInCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SheardResource> _Localizer;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly IAuthenticationService _authenticationService;





        #endregion
        #region Constructors
        public AuthenticationUserHandle(IStringLocalizer<SheardResource> stringLocalizer, IMapper mapper
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
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return BadRequst<string>(_Localizer[KeySharedResource.UserNameIsinvalid]);
            var singInUser = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!singInUser.IsCompletedSuccessfully)
                return BadRequst<string>(_Localizer[KeySharedResource.PasswordIsNotCorrect]);
            var accessToken = await _authenticationService.GetJWTToken(user);
            return Success(accessToken);



        }
        #endregion
    }
}
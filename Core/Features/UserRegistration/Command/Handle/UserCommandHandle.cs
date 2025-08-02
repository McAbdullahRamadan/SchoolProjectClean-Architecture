using AutoMapper;
using Core.Besec;
using Core.Features.UserRegistration.Command.Models;
using Core.Resource;
using Data.Entites.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Core.Features.UserRegistration.Command.Handle
{
    public class UserCommandHandle : ResponseHadlar,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>

    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SheardResource> _Localizer;
        private readonly UserManager<UserIdentity> _userManager;


        #endregion
        #region Constructors
        public UserCommandHandle(IStringLocalizer<SheardResource> stringLocalizer, IMapper mapper
            , UserManager<UserIdentity> userManage) : base(stringLocalizer)
        {
            _Localizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManage;

        }

        #endregion
        #region Handle Function

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if Email Is Exist
            var User = await _userManager.FindByEmailAsync(request.Email);
            // Email Exist
            if (User != null) return BadRequst<string>(_Localizer[KeySharedResource.EmailIsExist]);
            //if UserName Is Exist
            var UserByuserName = await _userManager.FindByNameAsync(request.FullName);
            //UserName Is Exist
            if (UserByuserName != null) return BadRequst<string>(_Localizer[KeySharedResource.UserNameIsExist]);
            //Mapping
            var UserMapp = _mapper.Map<UserIdentity>(request);
            //Create User
            var CrateResult = await _userManager.CreateAsync(UserMapp, request.Password);

            //Failed
            if (!CrateResult.Succeeded)
            {
                return BadRequst<string>(CrateResult.Errors.FirstOrDefault().Description);

            }
            //return
            return Created("");
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //Chack if User is Exist
            var Olduser = await _userManager.FindByIdAsync(request.Id.ToString());
            //return NotFuond
            if (Olduser == null)
                return NotFound<string>();
            //Mapping
            var newuser = _mapper.Map(request, Olduser);
            //Update
            var result = await _userManager.UpdateAsync(newuser);
            //Rsult is Not Success
            if (!result.Succeeded)
                return BadRequst<string>(_Localizer[KeySharedResource.UpdateFailed]);
            return Success((string)_Localizer[KeySharedResource.Update]);






        }
        #endregion
    }
}

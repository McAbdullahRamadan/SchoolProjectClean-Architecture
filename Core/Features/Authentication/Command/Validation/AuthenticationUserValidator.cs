using Core.Features.Authentication.Command.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Core.Features.Authentication.Command.Validation
{
    public class AuthenticationUserValidator : AbstractValidator<SignInCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;


        #endregion
        #region Construtors
        public AuthenticationUserValidator(IStringLocalizer<SheardResource> Localizer
          )
        {

            _Localizer = Localizer;

            ApplayValidationRule();
            ApplyCustomeValidatioonrules();

        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {

            //UserName Validation
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
            .NotNull().WithMessage(_Localizer[KeySharedResource.Requierd]);


            //Password Validation
            RuleFor(x => x.Password)
           .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
           .NotNull().WithMessage(_Localizer[KeySharedResource.Requierd]);



        }
        public void ApplyCustomeValidatioonrules()
        {


        }
        #endregion
    }
}

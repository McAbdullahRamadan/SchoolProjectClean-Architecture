using Core.Features.Authentication.Command.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Core.Features.Authentication.Command.Validation
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;


        #endregion
        #region Construtors
        public ResetPasswordValidator(IStringLocalizer<SheardResource> Localizer
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

            //Email Validation
            RuleFor(x => x.Email)
           .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
           .NotNull().WithMessage(_Localizer[KeySharedResource.Requierd])
           .EmailAddress().WithMessage("Your Email Is Invalid");

            //Password Validation
            RuleFor(x => x.Password)
           .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
           .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull])
           .MinimumLength(8).WithMessage(_Localizer[KeySharedResource.MinLength])
           .MaximumLength(16).WithMessage(_Localizer[KeySharedResource.MaxLength]);
            //ConfirmPassword Validation
            RuleFor(x => x.ConfirmPassword)

      .Equal(x => x.Password).WithMessage(_Localizer[KeySharedResource.PasswordNotEqualConfirmPassword]);

        }
        public void ApplyCustomeValidatioonrules()
        {


        }
        #endregion
    }
}

using Core.Features.UserRegistration.Command.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Core.Features.UserRegistration.Command.Validator
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {

        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;


        #endregion
        #region Construtors
        public ChangePasswordValidator(IStringLocalizer<SheardResource> Localizer)
        {

            _Localizer = Localizer;

            ApplayValidationRule();
            ApplyCustomeValidatioonrules();

        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {
            //FullName Validation
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
                .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull]);


            //CurrentPassword Validation
            RuleFor(x => x.CurrentPassword)
             .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
           .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull])
           .MinimumLength(8).WithMessage(_Localizer[KeySharedResource.MinLength])
           .MaximumLength(16).WithMessage(_Localizer[KeySharedResource.MaxLength]);

            //Password Validation
            RuleFor(x => x.NewPassword)
           .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
           .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull])
           .MinimumLength(8).WithMessage(_Localizer[KeySharedResource.MinLength])
           .MaximumLength(16).WithMessage(_Localizer[KeySharedResource.MaxLength]);

            //ConfirmPassword Validation
            RuleFor(x => x.ConfirmPassword)

      .Equal(x => x.NewPassword).WithMessage(_Localizer[KeySharedResource.PasswordNotEqualConfirmPassword]);

        }
        public void ApplyCustomeValidatioonrules()
        {


        }
        #endregion
    }

}

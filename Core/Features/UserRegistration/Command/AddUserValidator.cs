using Core.Features.UserRegistration.Command.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Core.Features.UserRegistration.Command
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;


        #endregion
        #region Construtors
        public AddUserValidator(IStringLocalizer<SheardResource> Localizer
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
            //FullName Validation
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
                .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull])
                .MaximumLength(15).WithMessage("Max Length is 15");
            //UserName Validation
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
            .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull])
            .MaximumLength(15).WithMessage("Max Length is 15");
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

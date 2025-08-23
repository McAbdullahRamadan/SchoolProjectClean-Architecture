using Core.Features.UserRegistration.Command.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Core.Features.UserRegistration.Command.Validator
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;


        #endregion
        #region Construtors
        public EditUserValidator(IStringLocalizer<SheardResource> Localizer
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



        }
        public void ApplyCustomeValidatioonrules()
        {


        }
        #endregion
    }
}

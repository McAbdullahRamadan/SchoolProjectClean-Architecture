using Core.Features.Authentication.Command.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Core.Features.Authentication.Command.Validation
{
    public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;


        #endregion
        #region Construtors
        public SendResetPasswordValidator(IStringLocalizer<SheardResource> Localizer
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
            .NotNull().WithMessage(_Localizer[KeySharedResource.Requierd]);




        }
        public void ApplyCustomeValidatioonrules()
        {


        }
        #endregion
    }
}

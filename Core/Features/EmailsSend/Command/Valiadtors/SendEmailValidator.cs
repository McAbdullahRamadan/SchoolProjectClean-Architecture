using Core.Features.EmailsSend.Command.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Core.Features.EmailsSend.Command.Valiadtors
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;


        #endregion

        #region Constructors
        public SendEmailValidator(IStringLocalizer<SheardResource> Localizer)
        {

            _Localizer = Localizer;

            ApplayValidationRule();
            ApplyCustomeValidatioonrules();

        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
                .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull])
                .EmailAddress().WithMessage("Email Invalid");

            RuleFor(x => x.Message)
                 .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
                 .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull]);

        }
        public void ApplyCustomeValidatioonrules()
        {


        }
        #endregion
    }
}

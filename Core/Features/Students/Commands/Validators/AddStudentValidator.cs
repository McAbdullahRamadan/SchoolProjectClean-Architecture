using Core.Features.Students.Commands.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentComand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SheardResource> _Localizer;

        #endregion

        #region Constructors
        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SheardResource> Localizer)
        {
            _studentService = studentService;
            _Localizer = Localizer;

            ApplayValidationRule();
            ApplyCustomeValidatioonrules();

        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {
            RuleFor(x => x.NameAR)
                .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
                .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull])
                .MaximumLength(15).WithMessage("Max Length is 15");
            RuleFor(x => x.Adreess)
            .NotEmpty().WithMessage("{PropertyName}Name is Not Empty")
            .NotNull().WithMessage("Name is Not Null")
            .MaximumLength(15).WithMessage("Max Length is 15");

        }
        public void ApplyCustomeValidatioonrules()
        {
            RuleFor(x => x.NameAR)
                .MustAsync(async (key, CancellationToken) => !await _studentService.IsNameExist(key))
                .WithMessage("Name Is Exist");

        }
        #endregion
    }
}

using Core.Features.Students.Commands.Models;
using FluentValidation;
using Service.Abstruct;

namespace Core.Features.Students.Commands.Validators
{
    public class EditValidatorStudent : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion

        #region Constructors
        public EditValidatorStudent(IStudentService studentService)
        {
            _studentService = studentService;
            ApplayValidationRule();
            ApplyCustomeValidatioonrules();
        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {
            RuleFor(x => x.NameAR)
                .NotEmpty().WithMessage("{PropertyName}Name is Not Empty")
                .NotNull().WithMessage("Name is Not Null")
                .MaximumLength(100).WithMessage("Max Length is 15");
            RuleFor(x => x.Adreess)
            .NotEmpty().WithMessage("{PropertyName}Name is Not Empty")
            .NotNull().WithMessage("Name is Not Null")
            .MaximumLength(100).WithMessage("Max Length is 15");

        }
        public void ApplyCustomeValidatioonrules()
        {
            RuleFor(x => x.NameAR)
                .MustAsync(async (model, key, CancellationToken) => !await _studentService.IsNameExistExcloudSelf(key, model.Id))
                .WithMessage("Name Is Exist");

        }
        #endregion
    }
}

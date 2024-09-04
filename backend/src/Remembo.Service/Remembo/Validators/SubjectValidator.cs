using FluentValidation;
using Remembo.Domain.Remembo.DTOs;

namespace Remembo.Service.Remembo.Validators;
internal class SubjectValidator : AbstractValidator<SubjectDto> {
    internal SubjectValidator() {
        RuleFor(m => m.Name)
            .NotEmpty().WithMessage("Subject name can not be empty.")
            .NotNull().WithMessage("Subject name is required.")
            .MinimumLength(2)
            .MaximumLength(50);
    }
}
using FluentValidation;
using Remembo.Domain.Remembo.DTOs;

namespace Remembo.Service.Remembo.Validators;
internal class ContentValidator : AbstractValidator<ContentDto> {
    public ContentValidator() {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Content name can not be empty.")
            .NotNull().WithMessage("Content name is required.")
            .MinimumLength(2)
            .MaximumLength(250);
        RuleFor(c => c.SubjectId)
            .NotEqual(Guid.Empty).WithMessage("SubjectId can not be empty.")
            .NotNull().WithMessage("SubjectId is required.");
    }
}

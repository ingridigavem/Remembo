using FluentValidation;
using Remembo.Domain.Remembo.DTOs;

namespace Remembo.Service.Remembo.Validators;
public class MatterValidator : AbstractValidator<MatterDto> {
    public MatterValidator() {
        RuleFor(m => m.Name)
            .NotEmpty().WithMessage("Matter name can not be empty.")
            .NotNull().WithMessage("Matter name is required.")
            .MinimumLength(2)
            .MaximumLength(50);
    }
}
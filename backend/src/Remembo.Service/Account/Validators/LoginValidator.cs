using FluentValidation;
using Remembo.Domain.Account.DTOs;

namespace Remembo.Service.Account.Validators;
internal class LoginValidator : AbstractValidator<LoginDto> {
    internal LoginValidator() {
        RuleFor(l => l.Email)
                .NotEmpty().WithMessage("email can not be empty.")
                .NotNull().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is not valid");

        RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password can not be empty.")
                .NotNull().WithMessage("Password is required.");
    }
}

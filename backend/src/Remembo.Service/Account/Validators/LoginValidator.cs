using FluentValidation;
using Remembo.Domain.Account.DTOs;

namespace Remembo.Service.Account.Validators;
public class LoginValidator : AbstractValidator<LoginDto> {
    public LoginValidator() {
        RuleFor(u => u.Email)
                .NotEmpty().WithMessage("email can not be empty.")
                .NotNull().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is not valid");

        RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password can not be empty.")
                .NotNull().WithMessage("Password is required.");
    }
}

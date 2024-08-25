using FluentValidation;
using Remembo.Domain.Account.DTOs;

namespace Remembo.Service.Account.Validators;
public class AccountValidator : AbstractValidator<UserDto> {
    public AccountValidator() {
        RuleFor(u => u.Name)
                .NotEmpty().WithMessage("User name can not be empty.")
                .NotNull().WithMessage("User name is required.")
                .MinimumLength(2)
                .MaximumLength(50);

        RuleFor(u => u.Email)
                .NotEmpty().WithMessage("User email can not be empty.")
                .NotNull().WithMessage("User email is required.")
                .EmailAddress().WithMessage("Email format is not valid");

        RuleFor(u => u.Password)
                .NotEmpty().WithMessage("User password can not be empty.")
                .NotNull().WithMessage("User password is required.")
                .MinimumLength(6).WithMessage("Password length must be at least 6.")
                .MaximumLength(16).WithMessage(" Password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
                .Matches(@"[!?$*.&@#%=+{}\[\]-]+").WithMessage("Password must contain at least one special character (! ? # * . @ % & $ - = + [ ]{ } ).");
    }
}
using ExpressCargo.Core.Constants;
using ExpressCargo.WebApp.Models.Dtos.Accounts;

namespace ExpressCargo.WebApp.Validations.Accounts;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(m => m.OldPassword)
                .NotEmpty()
                .WithMessage("OldPassword is required")
                .MinimumLength(StaticConfiguration.PASSWORD_MIN_LENGTH)
                .WithMessage("Invalid password format")
                .MaximumLength(StaticConfiguration.PASSWORD_MAX_LENGTH)
                .WithMessage("Invalid password format");

        RuleFor(m => m.NewPassword)
                .NotEmpty()
                .WithMessage("NewPassword is required")
                .MinimumLength(StaticConfiguration.PASSWORD_MIN_LENGTH)
                .WithMessage("Invalid password format")
                .MaximumLength(StaticConfiguration.PASSWORD_MAX_LENGTH)
                .WithMessage("Invalid password format");
    }
}
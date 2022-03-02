using ExpressCargo.Core.Constants;
using ExpressCargo.WebApp.Models.Dtos.Accounts;

namespace ExpressCargo.WebApp.Validations.Accounts;
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

        RuleFor(m => m.EmailAddress)
            .NotEmpty()
            .WithMessage("EmailAddress is required")
            .EmailAddress()
            .WithMessage("Invalid email address")
            .MaximumLength(StaticConfiguration.EMAIL_LENGTH)
            .WithMessage("Invalid emailAddress format");



        RuleFor(m => m.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(StaticConfiguration.PASSWORD_MIN_LENGTH)
            .WithMessage("Invalid password format")
            .MaximumLength(StaticConfiguration.PASSWORD_MAX_LENGTH)
            .WithMessage("Invalid password format");

    }
}

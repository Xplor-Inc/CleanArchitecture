using ExpressCargo.Core.Constants;
using ExpressCargo.WebApp.Models.Dtos.Accounts;

namespace ExpressCargo.WebApp.Validations.Accounts;

public class ForgetPasswordDtoValidator : AbstractValidator<ForgetPasswordDto>
{
    public ForgetPasswordDtoValidator()
    {
        RuleFor(m => m.EmailAddress)
            .NotEmpty()
            .WithMessage("EmailAddress is required")
            .EmailAddress()
            .WithMessage("Invalid email address")
            .MaximumLength(StaticConfiguration.EMAIL_LENGTH)
            .WithMessage("Invalid emailAddress format");
    }
}
using ExpressCargo.Core.Constants;
using ExpressCargo.WebApp.Models.Dtos.Accounts;

namespace ExpressCargo.WebApp.Validations.Accounts;

public class ForgetPasswordDtoValidator : AbstractValidator<ForgetPasswordDto>
{
    public ForgetPasswordDtoValidator()
    {
        ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

        RuleFor(m => m.EmailAddress)
            .NotEmpty()
            .WithMessage("EmailAddress is required")
            .EmailAddress()
            .WithMessage("Invalid email address")
            .MaximumLength(StaticConfiguration.EMAIL_LENGTH)
            .WithMessage("Invalid emailAddress format");
    }
}
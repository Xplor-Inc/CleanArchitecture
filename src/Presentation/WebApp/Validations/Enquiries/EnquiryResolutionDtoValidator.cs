using ExpressCargo.Core.Constants;
using ExpressCargo.WebApp.Models.Dtos.Enquiries;

namespace ExpressCargo.WebApp.Validations.Enquiries
{
    public class EnquiryResolutionDtoValidator : AbstractValidator<EnquiryResolutionDto>
    {
        public EnquiryResolutionDtoValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(m => m.Resolution)
               .NotEmpty()
               .WithMessage("Resolution is required")
               .MaximumLength(StaticConfiguration.COMMAN_LENGTH)
               .WithMessage("Resolution is too long");
        }
    }
}
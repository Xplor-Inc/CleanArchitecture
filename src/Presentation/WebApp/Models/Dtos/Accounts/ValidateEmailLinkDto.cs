namespace ExpressCargo.WebApp.Models.Dtos.Accounts;

public class ValidateEmailLinkDto : ForgetPasswordDto
{
    public string Resetlink     { get; set; }
}

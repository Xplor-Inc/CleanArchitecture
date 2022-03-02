namespace ExpressCargo.WebApp.Models.Dtos.Accounts;

public class ResetPasswordWithEmail : ValidateEmailLinkDto
{
    public string Password { get; set; }
}
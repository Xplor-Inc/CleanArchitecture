namespace CleanArchitecture.WebApp.Models.Dtos.Accounts;

public class ChangePasswordDto
{
    public string OldPassword       { get; set; }
    public string NewPassword       { get; set; }
}
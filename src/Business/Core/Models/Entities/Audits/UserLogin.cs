using CleanArchitecture.Core.Models.Entities.Users;

namespace CleanArchitecture.Core.Models.Entities.Audits;
public class UserLogin : Entity
{
    public string IP                 { get; set; } = string.Empty;
    public string ServerName         { get; set; } = string.Empty;
    public Guid   UserId             { get; set; }
    public string OperatingSystem    { get; set; } = string.Empty;
    public string Browser            { get; set; } = string.Empty;
    public string Device             { get; set; } = string.Empty;

    public virtual User User { get; set; } = new User();

}
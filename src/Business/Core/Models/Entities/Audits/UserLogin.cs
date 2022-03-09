using CleanArchitecture.Core.Models.Entities.Users;

namespace CleanArchitecture.Core.Models.Entities.Audits;
public class UserLogin : Entity
{
    public string       Browser            { get; set; } = string.Empty;
    public new long?    CreatedById        { get; set; }
    public string       Device             { get; set; } = string.Empty;
    public string?      Email              { get; set; }
    public string       IP                 { get; set; } = string.Empty;
    public bool         IsLoginSuccess     { get; set; }
    public bool         IsValidUser        { get; set; }
    public string       OperatingSystem    { get; set; } = string.Empty;
    public string       ServerName         { get; set; } = string.Empty;
    public long?        UserId             { get; set; }

    public virtual User? User          { get; set; } 

}
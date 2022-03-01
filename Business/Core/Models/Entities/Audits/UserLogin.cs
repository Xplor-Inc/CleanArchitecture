
using CleanArchitecture.Core.Models.Entities.Users;

namespace CleanArchitecture.Core.Models.Entities.Audits;
public class UserLogin : Entity
{
    public string IP                 { get; set; }
    public string ServerName         { get; set; }
    public Guid   UserId             { get; set; }
    public string OperatingSystem    { get; set; }
    public string Browser            { get; set; }
    public string Device             { get; set; }

    public virtual User User { get; set; }

}
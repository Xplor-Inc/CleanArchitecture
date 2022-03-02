using ExpressCargo.Core.Enumerations;

namespace ExpressCargo.WebApp.Extensions.Controllers;
public class ExpressCargoClaimsPrincipal
{
    public virtual UserRole    UserRole                { get; set; }
    public virtual Guid        UserId                  { get; set; }
}
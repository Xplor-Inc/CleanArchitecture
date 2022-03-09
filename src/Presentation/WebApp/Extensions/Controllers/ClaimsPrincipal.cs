using CleanArchitecture.Core.Enumerations;

namespace CleanArchitecture.WebApp.Extensions.Controllers;
public class CleanArchitectureClaimsPrincipal
{
    public virtual UserRole    UserRole                { get; set; }
    public virtual long        UserId                  { get; set; }
}
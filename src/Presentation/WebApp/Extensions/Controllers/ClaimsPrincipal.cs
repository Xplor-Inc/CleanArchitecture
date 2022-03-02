using CleanArchitecture.Core.Enumerations;

namespace CleanArchitecture.WebApp.Extensions.Controllers;
public class CleanArchitectureClaimsPrincipal
{
    public virtual UserRole    UserRole                { get; set; }
    public virtual Guid        UserId                  { get; set; }
}
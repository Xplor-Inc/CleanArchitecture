using CleanArchitecture.Core.Enumerations;
using CleanArchitecture.Core.Extensions;

namespace CleanArchitecture.WebApp;
public abstract class CleanArchitectureController : ControllerController
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
    }

    protected virtual CleanArchitectureClaimsPrincipal CleanArchitectureClaims { get; set; }
    protected virtual UserRole? CurrentRoleType => CleanArchitectureClaims != null ? CleanArchitectureClaims.UserRole : User.RoleType();
    protected virtual Guid      CurrentUserId   => CleanArchitectureClaims != null ? CleanArchitectureClaims.UserId : User.UserId();
}
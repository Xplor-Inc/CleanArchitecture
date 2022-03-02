using ExpressCargo.Core.Enumerations;
using ExpressCargo.Core.Extensions;

namespace ExpressCargo.WebApp;
public abstract class ExpressCargoController : ControllerController
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
    }

    protected virtual ExpressCargoClaimsPrincipal ExpressCargoClaims { get; set; }
    protected virtual UserRole? CurrentRoleType => ExpressCargoClaims != null ? ExpressCargoClaims.UserRole : User.RoleType();
    protected virtual Guid      CurrentUserId   => ExpressCargoClaims != null ? ExpressCargoClaims.UserId : User.UserId();
}
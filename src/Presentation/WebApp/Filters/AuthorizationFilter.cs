namespace ExpressCargo.WebApp.Filters;

public class AuthorizationFilter : IAuthorizationFilter
{
    readonly AuthorizationRequirement _requirement;
    public AuthorizationFilter(AuthorizationRequirement requirement)
    {
        _requirement = requirement;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // If the user is not authenticated, return a 401
        if (!context.HttpContext?.User?.Identity?.IsAuthenticated ?? false)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
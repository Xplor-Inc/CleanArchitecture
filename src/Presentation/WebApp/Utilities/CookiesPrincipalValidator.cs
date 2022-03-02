using System.Security.Claims;

namespace ExpressCargo.WebApp.Utilities;
public static class PrincipalValidator
{
    public static async Task ValidateAsync(CookieValidatePrincipalContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var userId = context.Principal?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.PrimarySid)?.Value;
        if (userId == null)
        {
            return;
        }
        var securityStamp   = context.Principal?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.AuthenticationInstant)?.Value;
        var dbContext       = context.HttpContext.RequestServices.GetRequiredService<ExpressCargoContext>();
        var user            = await dbContext.Users.FirstOrDefaultAsync(e => e.Id == Guid.Parse(userId));
        if (user == null || user.DeletedOn.HasValue || user.SecurityStamp != securityStamp)
        {
            context.RejectPrincipal();
            return;
        }
    }
}
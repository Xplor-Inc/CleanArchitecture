namespace ExpressCargo.Core.Extensions;
public static class ClaimsPrincipalExtensions
{
    public static UserRole? RoleType(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        if (principal.IsUnauthenticated())
        {
            return null;
        }

        var roleIdClaim = principal.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (roleIdClaim == null)
        {
            return null;
        }

        return (UserRole)Enum.Parse(typeof(UserRole), roleIdClaim.Value);
    }
  
    public static bool IsAuthenticated(this ClaimsPrincipal principal) => principal?.Identity?.IsAuthenticated ?? false;

    public static bool IsUnauthenticated(this ClaimsPrincipal principal) => !principal.IsAuthenticated();

    public static Guid UserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }


        var userIdClaim = principal.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
        if (userIdClaim == null)
        {
            return Guid.Empty;
        }

        return Guid.Parse(userIdClaim.Value);
    }
}

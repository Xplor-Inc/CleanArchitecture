namespace ExpressCargo.WebApp.Utilities;
public class AuthenticationUtils
{
    public static AuthenticationProperties GetAuthenticationProperties()
    {
        return new AuthenticationProperties
        {
            AllowRefresh    = true,
            IssuedUtc       = DateTimeOffset.Now,
            ExpiresUtc      = DateTimeOffset.Now.AddHours(24),
            IsPersistent    = true
        };
    }
}
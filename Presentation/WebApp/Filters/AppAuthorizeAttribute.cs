namespace CleanArchitecture.WebApp.Filters;

public class AppAuthorizeAttribute : TypeFilterAttribute
{
    public AppAuthorizeAttribute()
        : base(typeof(AuthorizationFilter)) =>
        Arguments = new[] { new AuthorizationRequirement() };
}

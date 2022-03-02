using ExpressCargo.Core.Interfaces.Data;
using ExpressCargo.Core.Models.Configuration;
using ExpressCargo.Core.Models.Entities.Users;
using ExpressCargo.Core.Models.Security;
using ExpressCargo.WebApp.Utilities;

namespace ExpressCargo.WebApp.Extensions;
public static class IServiceCollectionExtension
{
    public static void AddCookieAuthentication(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddSingleton((sp) => configuration.GetSection("Authentication").GetSection("Cookie").Get<CookieAuthenticationConfiguration>());

        var cookieConfig    = configuration.GetSection("Authentication:Cookie").Get<CookieAuthenticationConfiguration>();
        var cookie          = new CookieBuilder()
        {
            Name        = cookieConfig.CookieName,
            SameSite    = SameSiteMode.Strict
        };
        var cookieEvents = new CookieAuthenticationEvents
        {
            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Task.CompletedTask;
            },
            OnValidatePrincipal = PrincipalValidator.ValidateAsync
        };

        services.AddAuthentication(cookieConfig.AuthenticationScheme)
            .AddCookie(cookieConfig.AuthenticationScheme, options =>
            {
                options.Cookie = cookie;
                options.Events = cookieEvents;
            });
    }

    public static void AddContexts(this IServiceCollection services, string connectionString)
    {
        var loggerFactory = new Serilog.Extensions.Logging.SerilogLoggerFactory(Log.Logger, false);
        services.AddDbContext<ExpressCargoContext>(ServiceLifetime.Scoped);
        services.AddScoped((sp) => new ExpressCargoContext(connectionString, loggerFactory));
        services.AddScoped<DataContext<User>>((sp) => new ExpressCargoContext(connectionString, loggerFactory));
        services.AddScoped<IDataContext<User>>((sp) => new ExpressCargoContext(connectionString, loggerFactory));
        services.AddScoped<IContext>((sp) => new ExpressCargoContext(connectionString, loggerFactory));
        services.AddScoped<IFinanceManagerContext>((sp) => new ExpressCargoContext(connectionString, loggerFactory));
    }

    public static void AddConfigurationFiles(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddSingleton((sp) => configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
        services.AddSingleton((sp) => configuration.GetSection("StaticFileConfiguration").Get<StaticFileConfiguration>());
    }
}
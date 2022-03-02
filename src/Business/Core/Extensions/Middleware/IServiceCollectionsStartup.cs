using CleanArchitecture.Core.Conductors;
using CleanArchitecture.Core.Conductors.Users;
using CleanArchitecture.Core.Interfaces.Conductor;
using CleanArchitecture.Core.Interfaces.Conductors.Accounts;
using CleanArchitecture.Core.Interfaces.Utility.Security;
using CleanArchitecture.Core.Utilities.Security;

namespace CleanArchitecture.Core.Extensions.Middleware;
public static class IServiceColletionsStartup
{
    public static void AddUtilityResolver(this IServiceCollection services)
    {
        services.AddScoped<IAccountConductor,       AccountConductor>();
        services.AddScoped<IEncryption,             Encryption>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped(typeof(IRepositoryConductor<>), typeof(RepositoryConductor<>));
    }
}
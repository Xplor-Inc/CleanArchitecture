using ExpressCargo.Core.Conductors;
using ExpressCargo.Core.Conductors.Users;
using ExpressCargo.Core.Interfaces.Conductor;
using ExpressCargo.Core.Interfaces.Conductors.Accounts;
using ExpressCargo.Core.Interfaces.Utility.Security;
using ExpressCargo.Core.Utilities.Security;

namespace ExpressCargo.Core.Extensions.Middleware;
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
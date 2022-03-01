using CleanArchitecture.Core.Interfaces.Utility.Security;

namespace CleanArchitecture.WebApp.Extensions.SeedData;
public static class IApplicationBuilderExtensions
{
    public static void ConfigureSeedData(this IApplicationBuilder _, IServiceScope serviceScope)
    {
        var context = serviceScope.ServiceProvider.GetService<CleanArchitectureContext>();
        if (context == null) throw new InvalidOperationException("context");
        context.Database.SetCommandTimeout(10000);
        context.Database.Migrate();

        var encryption = serviceScope.ServiceProvider.GetService<IEncryption>();
        if (encryption != null)
            context.AddInitialData(encryption);
    }
}
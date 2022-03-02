using ExpressCargo.SqlServer.Repositories;
using ExpressCargo.Core.Interfaces.Data;

namespace ExpressCargo.SqlServer.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddSqlRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}
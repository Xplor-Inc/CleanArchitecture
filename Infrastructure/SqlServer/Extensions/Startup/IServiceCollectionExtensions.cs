using CleanArchitecture.SqlServer.Repositories;
using CleanArchitecture.Core.Interfaces.Data;

namespace CleanArchitecture.SqlServer.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddSqlRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}
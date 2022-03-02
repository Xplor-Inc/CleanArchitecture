using FinanceManager.Conductor.Conductors.Domain.Transaction;
using FinanceManager.Conductor.Conductors.Domain.Users;
using FinanceManager.Core.Interfaces.Conductor;
using FinanceManager.Core.Interfaces.Domain.Accounts;
using FinanceManager.Core.Interfaces.Domain.Transaction;
using FinanceManager.Core.Interfaces.Security;
using FinanceManager.Core.Utilities.Security;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Conductor.Extensions
{
    public static class IServiceColletionsStartup
    {
        public static IServiceCollection AddConductorResolver(this IServiceCollection services)
        {
            services.AddScoped<IAccountConductor,                           AccountConductor>();
            services.AddScoped<IEncryption,                                 Encryption>();
            services.AddScoped<IReportConductor,                            ReportConductor>();

            // Repository defaults
            services.AddScoped(typeof(IRepositoryConductor<>),           typeof(RepositoryConductor<>));
            return services;
        }
    }
}

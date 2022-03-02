using CleanArchitecture.SqlServer.Extensions;
using CleanArchitecture.SqlServer.Maps.Audits;
using CleanArchitecture.SqlServer.Maps.Users;
using CleanArchitecture.Core.Interfaces.Data;
using CleanArchitecture.Core.Models.Entities.Audits;
using CleanArchitecture.Core.Models.Entities.Users;
using CleanArchitecture.Core.Models.Entities.Enquiries;
using CleanArchitecture.SqlServer.Maps.Enquiries;

namespace CleanArchitecture.SqlServer;
public class CleanArchitectureContext : DataContext<User>, IFinanceManagerContext
{
    #region Properties
    public DbSet<AccountRecovery>           AccountRecoveries           { get; set; }
    public DbSet<Enquiry>                   Enquiries                   { get; set; }
    public DbSet<UserLogin>                 UserLogins                  { get; set; }
    #endregion

    #region Constructor
    public CleanArchitectureContext(string connectionString, ILoggerFactory loggerFactory)
        : base(connectionString, loggerFactory)
    {
    }

    public CleanArchitectureContext(IConnection connection, ILoggerFactory loggerFactory)
        : base(connection, loggerFactory)
    {
    }

    #endregion

    #region IFinanceManager Implementation
    IQueryable<AccountRecovery>         IFinanceManagerContext.AccountRecoveries        => AccountRecoveries;
    IQueryable<ChangeLog>               IFinanceManagerContext.ChangeLogs               => ChangeLogs;
    IQueryable<Enquiry>                 IFinanceManagerContext.Enquiries                => Enquiries;
    IQueryable<UserLogin>               IFinanceManagerContext.UserLogins               => UserLogins;

    #endregion

    public override void ConfigureMappings(ModelBuilder modelBuilder)
    {
        modelBuilder.AddMapping(new AccountRecoveryMap());
        modelBuilder.AddMapping(new ChangeLogMap());
        modelBuilder.AddMapping(new EnquiryMap());
        modelBuilder.AddMapping(new UserMap());
        modelBuilder.AddMapping(new UserLoginMap());

        base.ConfigureMappings(modelBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
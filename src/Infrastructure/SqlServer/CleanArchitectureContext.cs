using ExpressCargo.SqlServer.Extensions;
using ExpressCargo.SqlServer.Maps.Audits;
using ExpressCargo.SqlServer.Maps.Users;
using ExpressCargo.Core.Interfaces.Data;
using ExpressCargo.Core.Models.Entities.Audits;
using ExpressCargo.Core.Models.Entities.Users;
using ExpressCargo.Core.Models.Entities.Enquiries;
using ExpressCargo.SqlServer.Maps.Enquiries;

namespace ExpressCargo.SqlServer;
public class ExpressCargoContext : DataContext<User>, IFinanceManagerContext
{
    #region Properties
    public DbSet<AccountRecovery>           AccountRecoveries           { get; set; }
    public DbSet<Enquiry>                   Enquiries                   { get; set; }
    public DbSet<UserLogin>                 UserLogins                  { get; set; }
    #endregion

    #region Constructor
    public ExpressCargoContext(string connectionString, ILoggerFactory loggerFactory)
        : base(connectionString, loggerFactory)
    {
    }

    public ExpressCargoContext(IConnection connection, ILoggerFactory loggerFactory)
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
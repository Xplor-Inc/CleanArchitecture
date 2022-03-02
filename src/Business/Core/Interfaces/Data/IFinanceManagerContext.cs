using ExpressCargo.Core.Models.Entities.Audits;
using ExpressCargo.Core.Models.Entities.Enquiries;
using ExpressCargo.Core.Models.Entities.Users;

namespace ExpressCargo.Core.Interfaces.Data;
public interface IFinanceManagerContext : IContext
{
    IQueryable<AccountRecovery>           AccountRecoveries           { get; }
    IQueryable<ChangeLog>                 ChangeLogs                  { get; }
    IQueryable<Enquiry>                   Enquiries                   { get; }
    IQueryable<UserLogin>                 UserLogins                  { get; }
}
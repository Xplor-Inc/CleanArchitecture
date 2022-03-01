using CleanArchitecture.Core.Models.Entities.Audits;
using CleanArchitecture.Core.Models.Entities.Enquiries;
using CleanArchitecture.Core.Models.Entities.Users;

namespace CleanArchitecture.Core.Interfaces.Data;
public interface IFinanceManagerContext : IContext
{
    IQueryable<AccountRecovery>           AccountRecoveries           { get; }
    IQueryable<ChangeLog>                 ChangeLogs                  { get; }
    IQueryable<Enquiry>                   Enquiries                   { get; }
    IQueryable<UserLogin>                 UserLogins                  { get; }
}
using ExpressCargo.Core.Models.Entities.Users;

namespace ExpressCargo.Core.Interfaces.Data;
public interface IDataContext<TUser> : IContext
        where TUser : User
{
    IQueryable<User>    Users   { get; }
}
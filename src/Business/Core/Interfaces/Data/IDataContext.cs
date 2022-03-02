using CleanArchitecture.Core.Models.Entities.Users;

namespace CleanArchitecture.Core.Interfaces.Data;
public interface IDataContext<TUser> : IContext
        where TUser : User
{
    IQueryable<User>    Users   { get; }
}
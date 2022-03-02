using CleanArchitecture.Core.Models.Entities.Users;

namespace CleanArchitecture.Core.Interfaces.Conductors.Accounts;
public interface IAccountConductor
{
    Result<bool>    ActivateAccount(string emailAddress, string link, string password);
    Result<bool>    AccountRecovery(string emailAddress);
    Result<User?>   Authenticate(string emailAddress, string password);
    Result<User?>   ChangePassword(Guid userId, string oldPassword, string newPassword);
    Result<User?>   CreateAccount(User user, Guid createdById);
    Result<User?>   IsActivationLinkValid(string emailAddress, string link);
    Result<bool>    ResetPasswordByEmailLink(string emailAddress, string link, string password);
    Result<bool>    ValidateEmailLink(string emailAddress, string link);
}
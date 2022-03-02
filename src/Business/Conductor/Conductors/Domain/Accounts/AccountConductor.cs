using FinanceManager.Core.Extensions;
using FinanceManager.Core.Interfaces.Conductor;
using FinanceManager.Core.Interfaces.Domain.Accounts;
using FinanceManager.Core.Interfaces.Errors;
using FinanceManager.Core.Interfaces.Security;
using FinanceManager.Core.Models.Entities.Audits;
using FinanceManager.Core.Models.Entities.Users;
using FinanceManager.Core.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using UAParser;

namespace FinanceManager.Conductor.Conductors.Domain.Users
{
    public class AccountConductor : IAccountConductor
    {
        #region Properties
        public IEncryption                              Encryption                          { get; }
        public IRepositoryConductor<User>               UserRepository                      { get; }
        public IRepositoryConductor<AccountRecovery>    AccountRecoveryRepository           { get; }
        public IRepositoryConductor<UserLogin>          UserLoginRepository                 { get; }
        public IHttpContextAccessor                     HttpContext                         { get; }
        #endregion


        #region Constructor
        public AccountConductor(
            IEncryption                             encryption,
            IRepositoryConductor<User>              userRepository, 
            IRepositoryConductor<AccountRecovery>   accountRecoveryRepository,
            IRepositoryConductor<UserLogin>         userLoginRepository,
            IHttpContextAccessor                    httpContext
            )
        {
            Encryption                 = encryption;
            UserRepository             = userRepository;
            AccountRecoveryRepository  = accountRecoveryRepository;
            UserLoginRepository        = userLoginRepository;
            HttpContext                = httpContext;
        }
        #endregion 

        public IResult<User> Authenticate(string emailAddress, string password) => Do<User>.Try(r =>
        {
            var userResult = UserRepository.FindAll(w => w.EmailAddress == emailAddress && w.DeletedOn == null && w.IsAccountActivated);
            if (userResult.HasErrors)
            {
                r.AddErrors(userResult.Errors);
                return null;
            }
            var user = userResult.ResultObject.FirstOrDefault();
            if (user != null)
            {
                string passwordHash = Encryption.GenerateHash(password, user.PasswordSalt);
                if (passwordHash == user.PasswordHash)
                {
                    user.LastLoginDate = DateTimeOffset.Now;
                    var userUpdateCreateResult = UserRepository.Update(user, user.Id);
                    if (userUpdateCreateResult.HasErrors)
                    {
                        r.AddErrors(userUpdateCreateResult.Errors);
                        return null;
                    }

                    var userIp = HttpContext.HttpContext.Connection.RemoteIpAddress.ToString();
                    var request = HttpContext.HttpContext.Request;

                    var userAgent = string.Empty;
                    var operatingSystem = string.Empty;
                    var browser = string.Empty;
                    var device = string.Empty;

                    if (request != null && request.Headers.ContainsKey("User-Agent") && !string.IsNullOrWhiteSpace(request.Headers["User-Agent"]))
                    {
                        userAgent = request.Headers["User-Agent"].ToString();

                        var parsedUA = Parser.GetDefault().Parse(userAgent);
                        operatingSystem = $"{parsedUA.OS.Family} {parsedUA.OS.Major}{(parsedUA.OS.Minor.IsNullOrEmpty() ? "" : $".{parsedUA.OS.Minor}")}";
                        browser = $"{parsedUA.UA.Family} {parsedUA.UA.Major}{(parsedUA.UA.Minor.IsNullOrEmpty() ? "" : $".{parsedUA.UA.Minor}")}";
                        device = $"{parsedUA.Device.Family} {parsedUA.Device.Brand} {(parsedUA.Device.Model)}";
                    }
                    if (request.Headers.ContainsKey("X-Forwarded-For") && !string.IsNullOrWhiteSpace(request.Headers["X-Forwarded-For"]))
                    {
                        userIp = request.Headers["X-Forwarded-For"];
                    }

                    var userLogin = new UserLogin
                    {
                        IP = userIp,
                        ServerName = Environment.MachineName,
                        UserAgent = userAgent,
                        UserId = user.Id,
                        Browser = browser,
                        OperatingSystem = operatingSystem,
                        Device = device
                    };

                    var userLoginCreateResult = UserLoginRepository.Create(userLogin, user.Id);
                    if (userLoginCreateResult.HasErrors)
                    {
                        r.AddErrors(userLoginCreateResult.Errors);
                        return null;
                    }
                    return user;
                }
                else
                {
                    r.AddError("Invalid EmailAddress or Password");
                    return null;
                }
            }
            else
            {
                r.AddError("Invalid EmailAddress or Password");
                return null;
            }
        }).Result;

    }
}

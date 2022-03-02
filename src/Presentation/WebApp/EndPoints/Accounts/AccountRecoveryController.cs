using CleanArchitecture.Core.Interfaces.Conductors.Accounts;
using CleanArchitecture.WebApp.Models.Dtos.Accounts;

namespace CleanArchitecture.WebApp.EndPoints.Accounts;
[Route("api/1.0/accountrecovery")]
public class AccountRecoveryController : CleanArchitectureController
{
    #region Properties
    public IAccountConductor AccountConductor { get; }
    public IWebHostEnvironment Environment { get; }
    #endregion

    #region Contructor
    public AccountRecoveryController(
        IAccountConductor accountConductor,
        IWebHostEnvironment environment)
    {
        AccountConductor = accountConductor;
        Environment = environment;
    }
    #endregion

    [HttpPost("forgetpassword")]
    public IActionResult ForgetPassword([FromBody] ForgetPasswordDto dto)
    {
        var accountRecoveryResult = AccountConductor.AccountRecovery(dto.EmailAddress);
        if (accountRecoveryResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryResult.Errors);
        }
        return Ok(true, null);
    }

    [HttpPost("resetpassword")]
    public IActionResult ValidateEmailLink([FromBody] ValidateEmailLinkDto dto)
    {
        var accountRecoveryResult = AccountConductor.ValidateEmailLink(dto.EmailAddress, dto.Resetlink);
        if (accountRecoveryResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryResult.Errors);
        }
        return Ok(true, null);
    }

    [HttpPut("resetpassword")]
    public IActionResult ResetPasswordByEmailLink([FromBody] ResetPasswordWithEmail dto)
    {
        var accountRecoveryResult = AccountConductor.ResetPasswordByEmailLink(dto.EmailAddress, dto.Resetlink, dto.Password);
        if (accountRecoveryResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryResult.Errors);
        }
        return Ok(true, null);
    }

    #region Activate Account

    [HttpPost("accountactivation")]
    public IActionResult AccountActivation([FromBody] ValidateEmailLinkDto dto)
    {
        var accountRecoveryResult = AccountConductor.IsActivationLinkValid(dto.EmailAddress, dto.Resetlink);
        if (accountRecoveryResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryResult.Errors);
        }
        return Ok(true, null);
    }

    [HttpPut("accountactivation")]
    public IActionResult AccountActivation([FromBody] ResetPasswordWithEmail dto)
    {
        var accountRecoveryResult = AccountConductor.ActivateAccount(dto.EmailAddress, dto.Resetlink, dto.Password);
        if (accountRecoveryResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryResult.Errors);
        }
        return Ok(true, null);
    }
    #endregion
}
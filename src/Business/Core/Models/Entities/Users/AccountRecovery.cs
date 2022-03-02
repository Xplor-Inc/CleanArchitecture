namespace ExpressCargo.Core.Models.Entities.Users;
public class AccountRecovery : Auditable
{
    public Guid                 UserId                      { get; set; }
    public string               ResetLink                   { get; set; } = string.Empty;
    public DateTimeOffset       ResetLinkSentAt             { get; set; }
    public bool                 ResetLinkExpired            { get; set; }
    public int                  RetryCount                  { get; set; }
    public DateTimeOffset?      PasswordResetAt             { get; set; }
    public bool                 PasswordResetSuccessfully   { get; set; }
    public bool                 EmailSent                   { get; set; }

    public virtual User         User                        { get; set; } = new User();
}
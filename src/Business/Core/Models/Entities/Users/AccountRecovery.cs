namespace CleanArchitecture.Core.Models.Entities.Users;
public class AccountRecovery : Auditable
{
    public bool                 EmailSent                   { get; set; }
    public DateTimeOffset?      PasswordResetAt             { get; set; }
    public bool                 PasswordResetSuccessfully   { get; set; }
    public string               ResetLink                   { get; set; } = string.Empty;
    public bool                 ResetLinkExpired            { get; set; }
    public DateTimeOffset       ResetLinkSentAt             { get; set; }
    public int                  RetryCount                  { get; set; }
    public Guid                 UserId                      { get; set; }

    public virtual User         User                        { get; set; } = new User();
}
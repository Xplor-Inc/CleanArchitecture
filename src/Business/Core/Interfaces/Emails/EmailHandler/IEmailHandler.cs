namespace ExpressCargo.Core.Interfaces.Emails.EmailHandler;
public interface IEmailHandler
{
    bool Send(string message, string subject, string[] toEmails, string[]? attachments = null);
    bool Send(string message, string subject, string[] toEmails, string headerText, string[]? attachments = null);
    bool Send(string message, string subject, string[] toEmails, string[] cCEmail, string[]? attachments = null);
    bool Send(string message, string subject, string[] toEmails, string[] cCEmail, string headerText, string[]? attachments = null);
}

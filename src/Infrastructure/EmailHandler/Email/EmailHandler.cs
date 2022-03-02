using ExpressCargo.Core.Interfaces.Emails.EmailHandler;
using ExpressCargo.Core.Models.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ExpressCargo.Emails.Email;

public class EmailHandler : IEmailHandler
{
    readonly string pattern = @"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";

    private EmailConfiguration      Configuration   { get; }
    public ILogger<EmailHandler>    Logger          { get; }

    public EmailHandler(
        EmailConfiguration      configuration,
        ILogger<EmailHandler>   logger)
    {
        Configuration   = configuration;
        Logger          = logger;
    }
    public bool Send(string message, string subject, string[] toEmails, string[]? attachments = null)
    {
        ThrowErrroIfNull(message, subject, toEmails);
        MailMessage mailMessage = GetMailMessage(message, subject, Configuration.Header, attachments);

        bool ValiEmail = false;
        for (int i = 0; i < toEmails.Length; i++)
        {
            if (Regex.IsMatch(toEmails[i], pattern))
            {
                ValiEmail = true;
                mailMessage.To.Add(toEmails[i]);
            }
        }
        if (ValiEmail)
        {
            SendMailMessage(mailMessage);
        }

        return ValiEmail;
    }
    public bool Send(string message, string subject, string[] toEmails, string headerText, string[]? attachments = null)
    {
        ThrowErrroIfNull(message, subject, headerText, toEmails);
        MailMessage mailMessage = GetMailMessage(message, subject, headerText, attachments);

        bool ValiEmail = false;
        #region To Email Address
        for (int i = 0; i < toEmails.Length; i++)
        {
            if (Regex.IsMatch(toEmails[i], pattern))
            {
                ValiEmail = true;
                mailMessage.To.Add(toEmails[i]);
            }
        }
        #endregion

        if (ValiEmail)
        {
            SendMailMessage(mailMessage);
        }
        return ValiEmail;
    }
    public bool Send(string message, string subject, string[] toEmails, string[] cCEmails, string[]? attachments = null)
    {
        ThrowErrroIfNull(message, subject, toEmails, cCEmails);
        MailMessage mailMessage = GetMailMessage(message, subject, Configuration.Header, attachments);

        bool ValiEmail = false;
        #region To Email Address
        for (int i = 0; i < toEmails.Length; i++)
        {
            if (Regex.IsMatch(toEmails[i], pattern))
            {
                ValiEmail = true;
                mailMessage.To.Add(toEmails[i]);
            }
        }
        #endregion
        #region     CC Email Address
        for (int i = 0; i < cCEmails.Length; i++)
        {
            if (Regex.IsMatch(cCEmails[i], pattern))
            {
                mailMessage.To.Add(cCEmails[i]);
            }
        }
        #endregion

        if (ValiEmail)
        {
            SendMailMessage(mailMessage);
        }
        return ValiEmail;
    }
    public bool Send(string message, string subject, string[] toEmails, string[] cCEmails, string headerText, string[]? attachments = null)
    {
        ThrowErrroIfNull(message, subject, headerText, toEmails, cCEmails);
        MailMessage mailMessage = GetMailMessage(message, subject, headerText, attachments);

        bool ValiEmail = false;
        #region To Email Address
        for (int i = 0; i < toEmails.Length; i++)
        {
            if (Regex.IsMatch(toEmails[i], pattern))
            {
                ValiEmail = true;
                mailMessage.To.Add(toEmails[i]);
            }
        }
        #endregion
        #region     CC Email Address
        for (int i = 0; i < cCEmails.Length; i++)
        {
            if (Regex.IsMatch(cCEmails[i], pattern))
            {
                mailMessage.To.Add(cCEmails[i]);
            }
        }
        #endregion
        if (ValiEmail)
        {
            SendMailMessage(mailMessage);
        }
        return ValiEmail;
    }

    private MailMessage GetMailMessage(string message, string subject, string headerText, string[]? attachments = null)
    {
        MailMessage mailMessage = new()
        {
            From        = new MailAddress(Configuration.From, headerText),
            Subject     = subject,
            Body        = message,
            IsBodyHtml  = true,
            Priority    = MailPriority.High
        };
        if(attachments != null)
        {
            foreach (string attachment in attachments)
            {
                if (File.Exists(attachment))
                    mailMessage.Attachments.Add(new Attachment(attachment));
            }
        }
        mailMessage.ReplyToList.Add(Configuration.ReplyTo);
        return mailMessage;
    }
    private void SendMailMessage(MailMessage mailMessage)
    {
        if (!Configuration.SendEmail)
        {
            Logger.LogError("Email sending is not configured, please enable from AppSettings.json");
            return;
        }
        NetworkCredential nc = new(Configuration.UserName, Configuration.Password);
        using SmtpClient sc = new()
        {
            Host        = Configuration.Host,
            Port        = Configuration.Port,
            Credentials = nc,
            EnableSsl   = Configuration.EnableSsl
        };
        sc.Send(mailMessage);
        Logger.LogInformation($"Email sent successfully to {mailMessage.To[0].Address} with subject {mailMessage.Subject}");
    }
    private static void ThrowErrroIfNull(string message, params object[] param)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException(nameof(message));
        if (param != null && param.Length > 0)
        {
            for (int i = 0; i < param.Length; i++)
            {
                object p0 = param[i];
                if (string.IsNullOrEmpty(p0.ToString()))
                    throw new ArgumentNullException(nameof(p0));
            }
        }
    }
}


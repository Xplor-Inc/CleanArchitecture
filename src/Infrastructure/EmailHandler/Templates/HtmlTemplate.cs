using CleanArchitecture.Core.Interfaces.Emails.Templates;
using CleanArchitecture.Core.Models.Configuration;
using CleanArchitecture.Core.Models.Entities.Enquiries;
using CleanArchitecture.Core.Models.Entities.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using UAParser;

namespace Emails.Templates;
public class HtmlTemplate : IHtmlTemplate
{
    public string               HostName            { get; }
    public EmailConfiguration   EmailConfiguration  { get; }
    public IWebHostEnvironment  Environment         { get; }
    public IHttpContextAccessor HttpContext         { get; }

    public HtmlTemplate(
        EmailConfiguration      emailConfiguration,
        IWebHostEnvironment     environment,
        IHttpContextAccessor    httpContext)
    {
        EmailConfiguration  = emailConfiguration;
        Environment         = environment;
        HttpContext         = httpContext;

        var req = HttpContext.HttpContext?.Request;
        if (req == null) { throw new NullReferenceException(nameof(req)); }

        HostName = $"{req.Scheme}://{req.Host}";
    }

    public string ResetPassword(User user)
    {       
        string resetLink = $"{HostName}/{EmailConfiguration.Templates.ResetPasswordLink}/{user.Id}/{user.SecurityStamp}/{user.EmailAddress}";
        
        string templatePath = $"{Environment.WebRootPath}/{EmailConfiguration.Templates.ResetPasswordTemplate}";


        var (ipAddress, operatingSystem, browser, device) = GetUserAgent();

        string template = File.ReadAllText(templatePath);
        string emailbody = template.Replace("{{applicationUrl}}",   HostName)
                                   .Replace("{{PasswordResetUrl}}", resetLink)
                                   .Replace("{{Name}}",             user.FirstName)
                                   .Replace("{{OperatingSystem}}",  operatingSystem)
                                   .Replace("{{BrowserName}}",      browser)
                                   .Replace("{{IPAddress}}",        ipAddress)
                                   .Replace("{{Device}}",           device == "Other  " ? "": device);

        return emailbody;
    }

    public string EnquiryThanks(Enquiry enquiry)
    {
        string templatePath = $"{Environment.WebRootPath}/{EmailConfiguration.Templates.EnquiryTemplate}";

        string template = File.ReadAllText(templatePath);
        var (ipAddress, operatingSystem, browser, device) = GetUserAgent();
        string emailbody = template.Replace("{{applicationUrl}}",   HostName)
                                   .Replace("{{Name}}",             enquiry.Name)
                                   .Replace("{{Email}}",            enquiry.Email)
                                   .Replace("{{Subject}}",          enquiry.Subject)
                                   .Replace("{{Message}}",          enquiry.Message)
                                   .Replace("{{OperatingSystem}}",  operatingSystem)
                                   .Replace("{{BrowserName}}",      browser)
                                   .Replace("{{IPAddress}}",        ipAddress)
                                   .Replace("{{Device}}",           device == "Other  " ? "" : device);

        return emailbody;
    }

    public string AccountActivation(User user)
    {
        string activationLink = $"{HostName}/{EmailConfiguration.Templates.AccountActivationLink}/{user.Id}/{user.SecurityStamp}/{user.EmailAddress}";

        string templatePath = $"{Environment.WebRootPath}/{EmailConfiguration.Templates.AccountActivationTemplate}";

        string template = File.ReadAllText(templatePath);
        string emailbody = template.Replace("{{applicationUrl}}", HostName)
                                   .Replace("{{Name}}", user.FirstName)
                                   .Replace("{{AccountActivationLink}}", activationLink);

        return emailbody;
    }

    private (string? IpAddress, string? OperatingSystem, string? Browser, string? Device) GetUserAgent()
    {
        var req = HttpContext.HttpContext?.Request;
        if (req == null) { throw new NullReferenceException(nameof(req)); }

        var ipAddress       = HttpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        var operatingSystem = string.Empty;
        var browser         = string.Empty;
        var device          = string.Empty;

        if (req.Headers.ContainsKey("User-Agent") && !string.IsNullOrWhiteSpace(req.Headers["User-Agent"]))
        {
            string? userAgent   = req.Headers["User-Agent"].ToString();

            var parsedUA        = Parser.GetDefault().Parse(userAgent);
            operatingSystem     = $"{parsedUA.OS.Family} {parsedUA.OS.Major}";
            browser             = $"{parsedUA.UA.Family} {parsedUA.UA.Major}";
            device              = $"{parsedUA.Device.Family}";
        }
        if (req.Headers.ContainsKey("X-Forwarded-For") && !string.IsNullOrWhiteSpace(req.Headers["X-Forwarded-For"]))
        {
            ipAddress = req.Headers["X-Forwarded-For"];
        }
        return (ipAddress, operatingSystem, browser, device);
    }
}
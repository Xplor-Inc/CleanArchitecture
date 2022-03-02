using ExpressCargo.Core.Interfaces.Emails.EmailHandler;
using ExpressCargo.Core.Interfaces.Emails.Templates;
using ExpressCargo.Emails.Email;
using Emails.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressCargo.Emails.Extensions.Middleware;
public static class IServiceColletionsStartup
{
    public static void AddEmailHandler(this IServiceCollection services)
    {
        services.AddScoped<IEmailHandler,           EmailHandler>();
        services.AddScoped<IHtmlTemplate,           HtmlTemplate>();
    }
}
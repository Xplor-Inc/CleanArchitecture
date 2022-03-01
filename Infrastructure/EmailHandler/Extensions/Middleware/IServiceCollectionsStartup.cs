using CleanArchitecture.Core.Interfaces.Emails.EmailHandler;
using CleanArchitecture.Core.Interfaces.Emails.Templates;
using CleanArchitecture.Emails.Email;
using Emails.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Emails.Extensions.Middleware;
public static class IServiceColletionsStartup
{
    public static void AddEmailHandler(this IServiceCollection services)
    {
        services.AddScoped<IEmailHandler,           EmailHandler>();
        services.AddScoped<IHtmlTemplate,           HtmlTemplate>();
    }
}
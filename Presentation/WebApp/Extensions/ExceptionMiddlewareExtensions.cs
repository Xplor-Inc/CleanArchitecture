using CleanArchitecture.Core.Models.Errors;

namespace CleanArchitecture.WebApp.Extensions;
public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    Result<string> error = new()
                    {
                        Errors = new List<string>
                    {
                        contextFeature.Error.Message
                    }
                    };
                    if (contextFeature.Error.InnerException != null)
                    {
                        error.Errors.Add(contextFeature.Error.InnerException.Message);
                    }

                    var json = JsonConvert.SerializeObject(error, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                    await context.Response.WriteAsync(json);
                }
            });
        });
    }
}

using CleanArchitecture.Core.Extensions;
using CleanArchitecture.Core.Models.Errors;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CleanArchitecture.WebApp.Extensions.Validation;
public static class ModelStateExtensions
{
    public static Result<T> ToResult<T>(this ModelStateDictionary modelState)
    {
        var result = new Result<T>(default);

        foreach (var entry in modelState)
        {
            foreach (var err in entry.Value.Errors)
            {
                if (!string.IsNullOrWhiteSpace(err.ErrorMessage))
                {
                    result.AddError(err.ErrorMessage);
                }
                else if (err.Exception != null)
                {
                    result.AddError(err.Exception.Message);
                }
            }
        }

        return result;
    }
}
namespace CleanArchitecture.Core.Extensions;
public static class IResultExtensions
{
    public static string AddError<T>(this Result<T> result, string error)
    {
        if (result.Errors == null)
        {
            result.Errors = new List<string>();
        }

        result.Errors.Add(error);

        return error;
    }
    public static string AddExceptionError<T>(this Result<T> result, Exception exception)
    {
        var message = $"Exception : {exception.Message}\n{exception.StackTrace}";
        if (exception.InnerException != null)
            message += $"InnerException : {exception.InnerException.Message}\n{exception.InnerException.StackTrace}";
        return result.AddError(message);
    }
    public static List<string> AddErrors<T>(this Result<T> result, IEnumerable<string> errors)
    {
        if (errors == null || !errors.Any())
        {
            return result.Errors;
        }

        foreach (var error in errors)
        {
            result.AddError(error);
        }

        return result.Errors;
    }
    public static bool HasErrorsOrResultIsFalse(this Result<bool> result)
        => result.HasErrors || !result.ResultObject;

    public static bool HasErrorsOrResultIsNull<T>(this Result<T> result)
        => result.HasErrors || result.ResultObject == null;

    public static string GetErrors<T>(this Result<T> result, string delimiter = "\n")
    {
        if (!result.HasErrors)
        {
            return string.Empty;
        }

        delimiter = string.IsNullOrEmpty(delimiter) ? "\n" : delimiter;

        return string.Join(delimiter, result.Errors);
    }
}

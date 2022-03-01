using CleanArchitecture.WebApp.Extensions.Validation;

namespace CleanArchitecture.WebApp.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            // Return an IResult containing all validation errors
            context.Result = new BadRequestObjectResult(context.ModelState.ToResult<object>());
        }
    }
}

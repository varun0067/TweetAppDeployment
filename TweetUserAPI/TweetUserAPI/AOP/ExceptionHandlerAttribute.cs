using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TweetUserAPI.Exceptions;

namespace TweetUserAPI.AOP
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(InvalidDatabaseOperationException))
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            else if (context.Exception.GetType() == typeof(UserAlreadyExistsException))
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            else if (context.Exception.GetType() == typeof(InvalidCredentialsException))
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            else
            {
                context.Result = new StatusCodeResult(500);
            }
        }
    }
}

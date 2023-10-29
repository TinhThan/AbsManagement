using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AbsManagementAPI.Core.Exceptions
{
    /// <summary>
    /// Class handle exceptions
    /// </summary>
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilter()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>()
            {
                {typeof(CustomMessageException),HandleMessageException },
                {typeof(CustomValidateException),HandleValidateException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }
            HandleUnknowException(context);
            base.OnException(context);
        }

        private static void HandleUnknowException(ExceptionContext context)
        {
            var details = new ProblemDetails()
            {
                Title = MessageSystem.SERVER_ERROR,
                Status = StatusCodes.Status500InternalServerError,
                Detail = context.Exception.InnerException == null ? context.Exception.Message : context.Exception.InnerException.Message
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private static void HandleMessageException(ExceptionContext exceptionContext)
        {
            var exception = (CustomMessageException)exceptionContext.Exception ?? new CustomMessageException(MessageSystem.DATA_INVALID);
            exceptionContext.Result = new JsonResult(new ExceptionResponse()
            {
                Title = exception.Title,
                Description = exception.Description,
                Detail = exception.Detail,
            })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            exceptionContext.ExceptionHandled = true;
        }

        private static void HandleValidateException(ExceptionContext exceptionContext)
        {
            var exception = (CustomValidateException)exceptionContext.Exception ?? new CustomValidateException(MessageSystem.DATA_INVALID);
            var details = new ValidationProblemDetails(exception.Errors)
            {
                Title = exception.Title
            };
            exceptionContext.Result = new BadRequestObjectResult(details);
            exceptionContext.ExceptionHandled = true;
        }
    }
}

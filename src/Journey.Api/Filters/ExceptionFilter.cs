using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        ResponseErrorsJson response;

        // pattern matching to test and convert
        if (context.Exception is JourneyException journeyException)
        {
            context.HttpContext.Response.StatusCode = (int)journeyException.GetHttpErrorCode();
            response = new ResponseErrorsJson(journeyException.GetErrorMessages());
        }
        else
        {
            var list = new List<string> { ResourceErrorMessages.GENERAL_ERROR };
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            response = new ResponseErrorsJson(list);
        }

        context.Result = new ObjectResult(response);
    }
}
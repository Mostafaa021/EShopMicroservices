using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExceptionMessage = (string Detail, string Title, int StatusCode); // Aliases Feature in .NET
namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler (ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        public  async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Log Exception
            logger.LogError($"Error Message : {exception.Message} , Occurance Time  :{DateTime.UtcNow}");

            //  Pattern Matching C#12 Feature
            ExceptionMessage details = exception switch
            {
                InternalServerException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException =>
                (
                 exception.Message,
                 exception.GetType().Name,
                 httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestException =>
                (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundException =>
                (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                _ =>
                (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };
            // Asign to Object of ProblemDetails
            var ProblemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = httpContext.Request.Path
            };
            // Add to Trace Identifier for  ProblemDetails Extenstions
            ProblemDetails.Extensions.Add("Trace Id ", httpContext.TraceIdentifier);

            // If only this Validation Exception Then Add this to Responce
            if (exception is ValidationException validationException)
            {
                ProblemDetails.Extensions.Add("ValidationErrors", validationException?.Errors);
            }

             await httpContext.Response.WriteAsJsonAsync(ProblemDetails,cancellationToken);

            return  await ValueTask.FromResult(true); 
        }
    }
}

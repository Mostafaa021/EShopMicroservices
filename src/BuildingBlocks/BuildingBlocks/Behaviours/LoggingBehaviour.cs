using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponce>
        (ILogger<LoggingBehaviour<TRequest, TResponce>> logger)
        :IPipelineBehavior<TRequest, TResponce>
        where TRequest : notnull, IRequest<TResponce>
        where TResponce : notnull
    {
        public async Task<TResponce> Handle(TRequest request, RequestHandlerDelegate<TResponce> next, CancellationToken cancellationToken)
        {
            // Log Start  Request 
            logger.LogInformation($"[Start] Handle Request ={typeof(TRequest).Name} - Responce = {typeof(TResponce).Name} - Request Data = {request}");

            // Making Timer for Measuring Request
            var timer = new Stopwatch();
            timer.Start();

            var responce = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 30) // if the request is greater than 3 seconds, then log the warnings
                logger.LogWarning($"[PERFORMANCE] The request {typeof(TRequest).Name} took {timeTaken.Seconds} seconds.");

            logger.LogInformation($"[END] Handled {typeof(TRequest).Name} with {typeof(TResponce).Name}");

            return responce; 
        }
    }
}

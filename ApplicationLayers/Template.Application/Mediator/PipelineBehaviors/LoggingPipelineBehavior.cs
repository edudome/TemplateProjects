using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Template.Application.Mediator.Responses;

namespace Template.Application.Mediator.PipelineBehaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;
        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Request
            var logMsg = "#############################################################" + Environment.NewLine;
            logMsg += $"Request of Type ({typeof(TRequest).Name}) is starting." + Environment.NewLine;


            logMsg += $"Request data:" + Environment.NewLine;
            logMsg += "{" + Environment.NewLine;
            var myType = request?.GetType();
            if (myType != null)
            {
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                foreach (var prop in props)
                {
                    var propValue = prop.GetValue(request, null);
                    logMsg += $"{prop.Name} : {propValue}" + Environment.NewLine;
                }
            }
            logMsg += "}" + Environment.NewLine;

            // iniciamos cronometro
            var timer = Stopwatch.StartNew();

            // continuo en el Handler del Request original
            var result = await next();
            
            timer.Stop();

            // Response
            var response = (IResponse?)result;
            if (response != null)
            {
                logMsg += $"Response status: {(response.HayError ? "FAIL" : "OK")}" + Environment.NewLine;
                logMsg += $"Response message: {response.Mensaje}" + Environment.NewLine;
            }
            logMsg += $"Request has finished in {timer.ElapsedMilliseconds}ms." + Environment.NewLine;
            logMsg += "#############################################################" + Environment.NewLine;

            _logger.LogInformation(logMsg);

            return result;
        }
    }
}

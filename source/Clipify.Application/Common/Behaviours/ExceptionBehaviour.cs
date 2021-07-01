using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clipify.Application.Common.Behaviours
{
    public class ExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<ExceptionBehaviour<TRequest, TResponse>> _logger;

        public ExceptionBehaviour(ILogger<ExceptionBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return next();
            }
            catch (Exception e)
            {
                _logger.LogError(e, @"Request: Unhandled Exception for Request {Name} -> {@Request}",
                    typeof(TRequest).DeclaringType?.Name, request);
                throw;
            }
        }
    }
}

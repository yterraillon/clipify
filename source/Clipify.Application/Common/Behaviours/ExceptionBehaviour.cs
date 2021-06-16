using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Clipify.Application.Common.Behaviours
{
    public class ExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return next();
            }
            catch (Exception e)
            {
                // TODO: Serilog logging.
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

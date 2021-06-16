using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Users;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Clipify.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;

        private readonly ICurrentUserService _currentUserService;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest>> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var user = _currentUserService.GetCurrentUser();

            _logger.LogInformation($"Request: {typeof(TRequest).DeclaringType?.Name}\nUserId: {user.Id}\nUsername: {user.Username}\n Content:{request}");

            return Task.CompletedTask;
        }
    }
}
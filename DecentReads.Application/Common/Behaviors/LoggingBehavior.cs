using DecentReads.Application.Common.Interfaces.Services;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>

    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehavior(ILogger<LoggingBehavior< TRequest, TResponse>> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            string? userName = string.Empty;



            _logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);



            var result = await next();

            
            

            return result;
        }

        
    }
}

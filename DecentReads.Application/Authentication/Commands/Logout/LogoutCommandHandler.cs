using DecentReads.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Authentication.Commands.Logout
{
    public class LogoutCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly IUserRepository userRepository;

        public LogoutCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await userRepository.LogoutAsync(request.RefreshToken, request.UserId);
            return Unit.Value;
        }
    }
}

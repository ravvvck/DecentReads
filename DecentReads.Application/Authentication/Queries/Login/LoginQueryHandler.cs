
using DecentReads.Application.Common.Authentication;
using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Application.Exceptions;
using DecentReads.Contracts.Authentication;
using DecentReads.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<AuthenticationResult>
    {
    }
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly ITokenGenerator jwtTokenGenerator;
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;

        public LoginQueryHandler(ITokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }
        public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            if (userRepository.GetUserByEmail(request.Email) is not User user)
            {
                throw new BadRequestException("User does not exist");
            }

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new NotFoundException("Invalid username or password");
            }

            var token = jwtTokenGenerator.GenerateToken(user);
            var refreshToken = jwtTokenGenerator.GenerateRefreshToken();
            user.UpdateRefreshToken(refreshToken);
            await userRepository.UpdateAsync(user);

            return new AuthenticationResult(
                  user,
                  token,
                  refreshToken);
        }
    }
}

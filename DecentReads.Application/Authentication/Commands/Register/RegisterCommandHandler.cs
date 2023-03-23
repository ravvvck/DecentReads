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

namespace DecentReads.Application.Authentication.Commands.Register
{
    public record RegisterCommand
    (
            string Username,
            string Email,
            string Password,
            string confirmPassword
    ) : IRequest<AuthenticationResult>;



    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly ITokenGenerator jwtTokenGenerator;
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<DecentReads.Domain.Users.User> passwordHasher;

        public RegisterCommandHandler(ITokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher<DecentReads.Domain.Users.User> passwordHasher)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }
        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (command.Password != command.confirmPassword)
            {
                throw new BadRequestException("The confirmation password field must be the same as the password field.");
            }
            if (userRepository.GetUserByEmail(command.Email) is not null)
            {
                throw new BadRequestException("User already exist");
            }
            
            var user = DecentReads.Domain.Users.User.Create(command.Username, command.Email, command.Password);
            var hashedPassword = passwordHasher.HashPassword(user,command.Password);
            user.PasswordHash= hashedPassword;
            userRepository.Register(user);
            var token = jwtTokenGenerator.GenerateToken(user);
            var refreshToken = jwtTokenGenerator.GenerateRefreshToken();
            user.UpdateRefreshToken(refreshToken);
            userRepository.UpdateAsync(user);

            return new AuthenticationResult(
                   user,
                   token,
                   refreshToken);
        }
    }
    }


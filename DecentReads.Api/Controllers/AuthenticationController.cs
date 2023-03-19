using AutoMapper;
using DecentReads.Application.Authentication.Commands.RefreshToken;
using DecentReads.Application.Authentication.Commands.Register;
using DecentReads.Application.Authentication.Queries.Login;
using DecentReads.Application.Exceptions;
using DecentReads.Contracts.Authentication;
using DecentReads.Domain.Users.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace DecentReads.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        private void SetRefreshTokenInCookie(RefreshToken refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

        }

        [HttpPost("register")]
        
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            
            var authResult = await _mediator.Send(command);
            var response = _mapper.Map<AuthenticationResponse>(authResult);
            
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQuery query)
        {
            var authResult = await _mediator.Send(query); 
            var response = _mapper.Map<AuthenticationResponse>(authResult);
            SetRefreshTokenInCookie(authResult.RefreshToken);
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (refreshToken == null)
            {
                throw new BadRequestException("Refresh token is required.");
            }
            var command = _mapper.Map<RefreshTokenCommand>((refreshTokenRequest, refreshToken));
            var authResult = await _mediator.Send(command);
            var response = _mapper.Map<AuthenticationResponse>(authResult);
            return Ok(response);
        }

    }
}

using AutoMapper;
using DecentReads.Application.Authentication.Commands.Logout;
using DecentReads.Application.Authentication.Commands.RefreshToken;
using DecentReads.Application.Authentication.Commands.Register;
using DecentReads.Application.Authentication.Queries.Login;
using DecentReads.Application.Exceptions;
using DecentReads.Contracts.Authentication;
using DecentReads.Domain.Users.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace DecentReads.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
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

            var cookieOptionsNotHttpOnly = new CookieOptions
            {
                HttpOnly = false,
                Expires = refreshToken.Expires,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshTokenExist", "true", cookieOptionsNotHttpOnly);
        }

        private void ClearRefreshToken()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(-1),
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", "", cookieOptions);

            var cookieOptionsNotHttpOnly = new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTime.Now.AddDays(1),
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshTokenExist", "false", cookieOptionsNotHttpOnly);
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

        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshTokenRequest = new RefreshTokenRequest();
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

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var command = new LogoutCommand() { UserId = userId, RefreshToken = refreshToken };
            ClearRefreshToken();
            await _mediator.Send(command);
            return Ok();
        }

    }
}

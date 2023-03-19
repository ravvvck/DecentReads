using AutoMapper;
using DecentReads.Application.Authentication.Commands.RefreshToken;
using DecentReads.Application.Authentication.Commands.Register;
using DecentReads.Contracts.Authentication;
using Microsoft.Extensions.Logging;

namespace GameHost.Api.Mapping
{
    public class AuthenticationMappingConfig : Profile
    { 
        public AuthenticationMappingConfig() 
        {
            CreateMap<AuthenticationResult, AuthenticationResponse>()
                .ForMember(result => result.Token, r => r.MapFrom(response => response.Token));

            CreateMap<RegisterRequest, RegisterCommand>();

            
            CreateMap<(RefreshTokenRequest request, string token), RefreshTokenCommand>()
                .ForMember(dst => dst.RefreshToken, r => r.MapFrom(src => src.token));


        }
    }
}


using DecentReads.Application.Common.Authentication;
using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Application.Common.Interfaces.Services;
using DecentReads.Infrastructure.Authentication;
using DecentReads.Infrastructure.Persistence;
using DecentReads.Infrastructure.Persistence.Repositories;
using DecentReads.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DecentReads.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            

            services.AddDbContext<DecentReadsDbContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("DecentReadsDatabase")))
            options.UseSqlite(configuration.GetConnectionString("SqlLite")));

            configuration.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });


            return services;
        }

    }
}

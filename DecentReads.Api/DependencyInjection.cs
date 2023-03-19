using DecentReads.Api.Mapping;
using MediatR;

namespace DecentReads.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddMappings();

            return services;
        }

    }
}

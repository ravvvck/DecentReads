using DecentReads.Application.Contracts.Persistence;
using DecentReads.Application.Profiles;
using DecentReads.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DecentReadsDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DecentReadsConnectionString"));
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped <IBookRepository, BookRepository> ();
            services.AddScoped<IAuthorRepository, AuthorRepository>();


            return services;
        }
    }
}

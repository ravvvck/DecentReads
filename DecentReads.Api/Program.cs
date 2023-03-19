using DecentReads.Api;
using DecentReads.Api.Middleware;
using DecentReads.Application;
using DecentReads.Infrastructure;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
{
    //builder.Services.AddTransient<ErrorHandlingMiddleware>();
    builder.Services.AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddPresentation();
    builder.Services.AddControllers();


}



var app = builder.Build();
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}

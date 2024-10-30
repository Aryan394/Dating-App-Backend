using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // Add services to the container (Dependency Injection container).
        services.AddControllers(); // Adding controller services to handle HTTP requests and responses

        // Configuring the database context to use SQLite
        services.AddDbContext<DataContext>(opt =>
        {
            // Use SQLite database server and fetch the connection string from the config file
            // The key "DefaultConnection" identifies the connection string in the configuration (e.g., appsettings.json)
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        // services.AddEndpointsApiExplorer(); // Service for automatically discovering endpoints (optional)
        // services.AddSwaggerGen(); // Service for generating Swagger documentation (optional)

        services.AddCors(); // Adding Cross-Origin Resource Sharing (CORS) service to allow API access from other origins

        // Adding a scoped service for ITokenService, mapping it to the TokenService implementation
        // Scoped services are created once per client request and reused throughout the request.
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}

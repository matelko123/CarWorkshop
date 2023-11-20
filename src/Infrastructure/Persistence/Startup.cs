using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Initialization;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

internal static class Startup
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration confuguration)
    {
        string connectionString = confuguration.GetConnectionString("Default")
                                  ?? throw new ArgumentException("Connection string cannot be empty.");

        return services
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
            .AddTransient<IDatabaseInitializer, DatabaseInitializer>()
            .AddRepositories();
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>))
            .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
    }
}
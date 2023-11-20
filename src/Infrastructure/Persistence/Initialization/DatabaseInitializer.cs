using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Initialization;

internal class DatabaseInitializer : IDatabaseInitializer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            if (context.Database.GetMigrations().Any())
            {
                if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    _logger.LogInformation("Applying Migrations.");
                    await context.Database.MigrateAsync(cancellationToken);
                }
                    
                if (await context.Database.CanConnectAsync(cancellationToken))
                {
                    _logger.LogInformation("Connection to Database Succeeded.");

                    // await _dbSeeder.SeedDatabaseAsync(_dbContext, cancellationToken);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            throw;
        }
    }
}
namespace DigitalWallet.Infrastructure;

public static class InfrastructureDependecyInjection
{
    public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DigitalWalletDbContext>(options =>
        {
            options.UseNpgsql(configuration.ConnectionString())
                .ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        });
    }

    public static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();

        services.AddScoped<IWalletReadOnlyRepository, WalletRepository>();
        services.AddScoped<IWalletWriteOnlyRepository, WalletRepository>();

        services.AddScoped<ITransactionReadOnlyRepository, TransactionRepository>();
        services.AddScoped<ITransactionWriteOnlyRepository, TransactionRepository>();
    }

    public static void ApplyMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
            .CreateLogger("Migration");

        try
        {
            var context = scope.ServiceProvider.GetRequiredService<DigitalWalletDbContext>();
            context.Database.Migrate();
            logger.LogInformation("Migrations applied successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying migrations.");
        }
    }

    public static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccessTokenGenerator>(provider => new JwtTokenGenerator(
            configuration.ExpirationTimeInMinutes(),
            configuration.SigningKey()));

        services.AddScoped<IAccessTokenValidator>(provider => new JwtTokenValidator(configuration.SigningKey()));
    }

    public static void AddLoggedUser(IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }

    public static void AddPasswordEncripter(IServiceCollection services)
    {
        services.AddScoped<IPasswordEncripter>(option => new BCryptNet());
    }

    public static void AddRedis(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICacheService, RedisCacheService>();

        var redisConnectionString = configuration.RedisConnectionString();

        if (redisConnectionString.NotEmpty())
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });
        }
    }
}

namespace DigitalWallet.Infrastructure.Extensions;

public static class ConfigurationExtension
{
    public static bool IsUnitTestEnviroment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }

    public static string ConnectionString(this IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;

        return connectionString.Empty()
           ? Environment.GetEnvironmentVariable("CONNECTION_MYSQL_SERVER") ?? string.Empty
           : connectionString ?? string.Empty!;
    }

    public static uint ExpirationTimeInMinutes(this IConfiguration configuration)
    {
        return configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
    }

    public static string SigningKey(this IConfiguration configuration)
    {
        return configuration.GetValue<string>("Settings:Jwt:SigningKey")!;
    }

    public static string RedisConnectionString(this IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("Settings:Redis:ConnectionString");

        return connectionString.Empty()
            ? Environment.GetEnvironmentVariable("CONNECTION_REDIS") ?? string.Empty
            : connectionString ?? string.Empty;
    }
}

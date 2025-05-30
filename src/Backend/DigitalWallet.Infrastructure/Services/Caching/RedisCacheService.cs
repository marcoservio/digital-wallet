﻿namespace DigitalWallet.Infrastructure.Services.Caching;

public class RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger) : ICacheService
{
    private readonly ILogger<RedisCacheService> _logger = logger;
    private readonly IDistributedCache _cache = cache;
    private readonly DistributedCacheEntryOptions _options = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
        SlidingExpiration = TimeSpan.FromMinutes(2),
    };

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };

    public async Task<T> GetAsync<T>(string key)
    {
        try
        {
            var value = await _cache.GetStringAsync(key);

            if (value.Empty())
                return default!;

            var obj = JsonSerializer.Deserialize<T>(value!, _jsonOptions);

            _logger.LogInformation("Valor da chave {CacheKey} obtido com sucesso da cache.", key);

            return obj!;
        }
        catch (RedisConnectionException)
        {
            _logger.LogError("Exceção ao tentar conectar com Redis ao obter chave {CacheKey}.", key);
            return default!;
        }
    }

    public async Task SetAsync<T>(string key, T obj)
    {
        try
        {
            var value = JsonSerializer.Serialize(obj, _jsonOptions);

            await _cache.SetStringAsync(key, value, _options);

            _logger.LogInformation("Valor da chave {CacheKey} definido com sucesso na cache.", key);
        }
        catch (RedisConnectionException ex)
        {
            _logger.LogWarning(ex, "Falha na conexão com a cache Redis para a chave: {CacheKey}", key);
        }
    }
}

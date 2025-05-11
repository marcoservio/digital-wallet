namespace CommonTestUtilities.Cache;

public class CacheServiceBuilder
{
    private readonly Mock<ICacheService> _cache;

    public CacheServiceBuilder() => _cache = new Mock<ICacheService>();

    public CacheServiceBuilder GetAsync<T>(T obj, bool withCache = false)
    {
        if (withCache)
            _cache.Setup(x => x.GetAsync<T>(It.IsAny<string>())).ReturnsAsync(obj);

        return this;
    }

    public ICacheService Build() => _cache.Object;
}

using CommonTestUtilities.Cache;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private DigitalWallet.Domain.Entities.User _user = default!;
    private DigitalWallet.Domain.Entities.Wallet _toWallet = default!;
    private DigitalWallet.Domain.Entities.Wallet _fromWallet = default!;
    private DigitalWallet.Domain.Entities.Transaction _transaction = default!;
    private string _password = string.Empty;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DigitalWalletDbContext>));

                if (descriptor is not null)
                    services.Remove(descriptor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<DigitalWalletDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                StartDatabase(services);

                services.AddScoped(option => new CacheServiceBuilder().Build());
            });
    }

    public string GetEmail() => _user.Email;

    public string GetName() => _user.Name;

    public string GetPassword() => _password;

    public Guid GetUserIdentifier() => _user.UserIdentifier;

    public decimal GetBalance() => _fromWallet.Balance;

    public Guid GetFromWalletKey() => _fromWallet.WalletKey;

    public Guid GetToWalletKey() => _toWallet.WalletKey;

    public DateTime GetTransactionDate() => _transaction.CreatedAt;

    private void StartDatabase(IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DigitalWalletDbContext>();

        context.Database.EnsureDeleted();

        (_user, _password) = UserBuilder.Build();

        _fromWallet = WalletBuilder.Build(_user);
        _toWallet = WalletBuilder.Build(id: 2);

        _transaction = TransactionBuilder.Build(_fromWallet, _toWallet);

        context.Users.Add(_user);

        context.Wallets.Add(_fromWallet);
        context.Wallets.Add(_toWallet);

        context.Transactions.Add(_transaction);

        context.SaveChanges();
    }
}

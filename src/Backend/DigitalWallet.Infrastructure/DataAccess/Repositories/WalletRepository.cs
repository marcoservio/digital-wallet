namespace DigitalWallet.Infrastructure.DataAccess.Repositories;

public class WalletRepository(DigitalWalletDbContext context) : IWalletReadOnlyRepository, IWalletWriteOnlyRepository
{
    private readonly DigitalWalletDbContext _context = context;

    public async Task Add(Wallet wallet) => await _context.Wallets.AddAsync(wallet);

    public async Task<Wallet> GetByUserId(long id) => await _context.Wallets.FirstAsync(w => w.IsActive && w.UserId == id);

    public async Task<Wallet> GetByWalletKey(Guid key) => await _context.Wallets.FirstAsync(w => w.IsActive && w.WalletKey.Equals(key));

    public void Update(Wallet wallet) => _context.Wallets.Update(wallet);
}

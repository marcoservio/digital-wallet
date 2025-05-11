namespace DigitalWallet.Infrastructure.DataAccess;

public class UnitOfWork(DigitalWalletDbContext context) : IUnitOfWork
{
    private readonly DigitalWalletDbContext _context = context;

    public async Task Commit() => await _context.SaveChangesAsync();
}

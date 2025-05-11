namespace DigitalWallet.Infrastructure.DataAccess.Repositories;

public class TransactionRepository(DigitalWalletDbContext context) : ITransactionWriteOnlyRepository, ITransactionReadOnlyRepository
{
    private readonly DigitalWalletDbContext _context = context;

    public async Task Add(Transaction transaction) => await _context.Transactions.AddAsync(transaction);

    public async Task<IList<Transaction>> Filter(User user, FilterTransferDto filter)
    {
        var query = _context
            .Transactions
            .AsNoTracking()
            .Include(t => t.FromWallet)
            .Include(t => t.ToWallet)
            .Where(t => t.IsActive &&
                (t.FromWallet!.UserId == user.Id || t.ToWallet!.UserId == user.Id));

        if (filter.StartDate.HasValue)
        {
            var startDateUtc = DateTime.SpecifyKind(filter.StartDate.Value.Date, DateTimeKind.Utc);
            query = query.Where(t => t.CreatedAt >= startDateUtc);
        }

        if (filter.EndDate.HasValue)
        {
            var endDateUtc = DateTime.SpecifyKind(filter.EndDate.Value.Date.AddDays(1).AddMilliseconds(-1), DateTimeKind.Utc);
            query = query.Where(t => t.CreatedAt <= endDateUtc);
        }

        return await query.ToListAsync();
    }

    public void Update(Transaction transaction) => _context.Transactions.Update(transaction);
}

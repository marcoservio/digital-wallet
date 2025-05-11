namespace DigitalWallet.Infrastructure.DataAccess.Repositories;

public class UserRepository(DigitalWalletDbContext context) : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly DigitalWalletDbContext _context = context;

    public async Task Add(User user) => await _context.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) =>
        await _context.Users.AnyAsync(u => u.Email.Equals(email) && u.IsActive);

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) =>
        await _context.Users.AnyAsync(u => u.IsActive && u.UserIdentifier.Equals(userIdentifier));

    public async Task<User?> GetByEmailAndPassword(string email, string password) =>
        (await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.IsActive && user.Email.Equals(email) && user.Password.Equals(password)))!;

    public Task<User?> GetByEmail(string email)
    {
        return _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.IsActive && user.Email.Equals(email));
    }
}

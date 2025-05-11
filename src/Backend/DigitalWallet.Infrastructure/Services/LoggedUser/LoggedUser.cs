namespace DigitalWallet.Infrastructure.Services.LoggedUser;

public class LoggedUser(DigitalWalletDbContext context, ITokenProvider tokenProvider) : ILoggedUser
{
    private readonly DigitalWalletDbContext _context = context;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _context
            .Users
            .AsNoTracking()
            .FirstAsync(u => u.IsActive && u.UserIdentifier.Equals(userIdentifier));
    }
}

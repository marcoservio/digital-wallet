namespace DigitalWallet.Infrastructure.Security.Tokens;

public abstract class JwtTokenHandler
{
    protected static SymmetricSecurityKey SecurityKey(string signingKey) => new(Encoding.UTF8.GetBytes(signingKey));
}

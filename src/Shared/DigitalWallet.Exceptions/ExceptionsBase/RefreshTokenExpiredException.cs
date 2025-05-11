namespace DigitalWallet.Exceptions.ExceptionsBase;

public class RefreshTokenExpiredException() : DigitalWalletException(ResourceMessagesException.EXPIRED_SESSION)
{
    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}

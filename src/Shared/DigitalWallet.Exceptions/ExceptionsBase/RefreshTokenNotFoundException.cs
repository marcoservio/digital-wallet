namespace DigitalWallet.Exceptions.ExceptionsBase;

public class RefreshTokenNotFoundException() : DigitalWalletException(ResourceMessagesException.INVALID_SESSION)
{
    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}

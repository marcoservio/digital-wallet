namespace DigitalWallet.Exceptions.ExceptionsBase;

public class TokenOnRequestException() : DigitalWalletException(ResourceMessagesException.NO_TOKEN)
{
    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}

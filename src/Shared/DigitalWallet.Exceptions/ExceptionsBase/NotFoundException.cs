namespace DigitalWallet.Exceptions.ExceptionsBase;

public class NotFoundException(string message) : DigitalWalletException(message)
{
    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}

namespace DigitalWallet.Exceptions.ExceptionsBase;

public class InvalidLoginException() : DigitalWalletException(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
{
    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}

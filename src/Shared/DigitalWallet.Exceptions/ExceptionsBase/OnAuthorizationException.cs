namespace DigitalWallet.Exceptions.ExceptionsBase;

public class OnAuthorizationException() : DigitalWalletException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE)
{
    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}

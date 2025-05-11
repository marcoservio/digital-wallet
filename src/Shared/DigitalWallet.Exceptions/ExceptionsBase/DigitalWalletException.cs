namespace DigitalWallet.Exceptions.ExceptionsBase;

public abstract class DigitalWalletException(string message) : SystemException(message)
{
    public abstract IList<string> GetErrorMessages();

    public abstract HttpStatusCode GetStatusCode();
}

namespace DigitalWallet.Exceptions.ExceptionsBase;

public class OnValidationException(IList<string> errorMessages) : DigitalWalletException(string.Empty)
{
    private readonly IList<string> _errorMessages = errorMessages;

    public override IList<string> GetErrorMessages() => _errorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}

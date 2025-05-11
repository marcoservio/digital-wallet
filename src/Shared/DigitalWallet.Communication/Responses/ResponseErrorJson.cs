namespace DigitalWallet.Communication.Responses;

public class ResponseErrorJson
{
    public ResponseErrorJson(IList<string> errors) => Errors = errors;

    public ResponseErrorJson(string error) => Errors = [error];

    public IList<string> Errors { get; set; }

    public bool TokenIsExpired { get; set; }
}

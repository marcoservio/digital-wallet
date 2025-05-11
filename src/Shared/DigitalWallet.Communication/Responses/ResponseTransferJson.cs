namespace DigitalWallet.Communication.Responses;

public class ResponseTransferJson
{
    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public string FromWallet { get; set; } = string.Empty;

    public string ToWallet { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime Date { get; set; }
}

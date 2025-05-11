namespace DigitalWallet.Communication.Requests;

public class RequestTransferJson
{
    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public Guid ToWalletKey { get; set; }
}

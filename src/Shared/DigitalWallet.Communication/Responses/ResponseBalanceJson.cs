namespace DigitalWallet.Communication.Responses;

public class ResponseBalanceJson
{
    public Guid WalletKey { get; set; }

    public decimal Balance { get; set; }
}

namespace DigitalWallet.Domain.Entities;

public class Transaction : EntityBase
{
    public long FromWalletId { get; set; }

    public long ToWalletId { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public TransactionStatus Status { get; set; }

    public Wallet? FromWallet { get; set; }

    public Wallet? ToWallet { get; set; }
}

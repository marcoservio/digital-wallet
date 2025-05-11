namespace DigitalWallet.Domain.Entities;

public class Wallet : EntityBase
{
    public long UserId { get; set; }

    public decimal Balance { get; set; }

    public Guid WalletKey { get; set; } = Guid.NewGuid();

    public byte[] ? RowVersion { get; set; } = [];

    public User? User { get; set; }

    public ICollection<Transaction> SentTransactions { get; set; } = [];

    public ICollection<Transaction> ReceivedTransactions { get; set; } = [];
}

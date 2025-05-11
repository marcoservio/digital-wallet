namespace DigitalWallet.Domain.Repositories.Wallet;

public interface IWalletWriteOnlyRepository
{
    Task Add(Entities.Wallet wallet);

    void Update(Entities.Wallet wallet);
}

namespace DigitalWallet.Domain.Repositories.Wallet;

public interface IWalletReadOnlyRepository
{
    Task<Entities.Wallet> GetByUserId(long id);

    Task<Entities.Wallet> GetByWalletKey(Guid key);
}

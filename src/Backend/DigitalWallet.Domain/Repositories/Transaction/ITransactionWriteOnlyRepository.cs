namespace DigitalWallet.Domain.Repositories.Transaction;

public interface ITransactionWriteOnlyRepository
{
    Task Add(Entities.Transaction transaction);

    void Update(Entities.Transaction transaction);
}

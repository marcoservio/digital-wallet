namespace DigitalWallet.Domain.Repositories.Transaction;

public interface ITransactionReadOnlyRepository
{
    Task<IList<Entities.Transaction>> Filter(Entities.User user, FilterTransferDto filter);
}

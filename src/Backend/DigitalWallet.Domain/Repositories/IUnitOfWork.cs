namespace DigitalWallet.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}

using DigitalWallet.Domain.Dtos;
using DigitalWallet.Domain.Repositories.Transaction;

namespace CommonTestUtilities.Repositories;

public class TransactionReadOnlyRepositoryBuilder
{
    private readonly Mock<ITransactionReadOnlyRepository> _repository;

    public TransactionReadOnlyRepositoryBuilder() => _repository = new Mock<ITransactionReadOnlyRepository>();

    public TransactionReadOnlyRepositoryBuilder Filter(IList<Transaction> transactions)
    {
        if (transactions is not null)
            _repository.Setup(r => r.Filter(It.IsAny<User>(), It.IsAny<FilterTransferDto>())).ReturnsAsync(transactions);

        return this;
    }

    public ITransactionReadOnlyRepository Build() => _repository.Object;
}

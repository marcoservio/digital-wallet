using DigitalWallet.Domain.Repositories.Transaction;

namespace CommonTestUtilities.Repositories;

public class TransactionWriteOnlyRepositoryBuilder
{
    public static ITransactionWriteOnlyRepository Build()
    {
        var mock = new Mock<ITransactionWriteOnlyRepository>();

        return mock.Object;
    }
}

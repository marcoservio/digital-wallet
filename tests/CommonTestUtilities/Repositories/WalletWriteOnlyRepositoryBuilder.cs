namespace CommonTestUtilities.Repositories;

public class WalletWriteOnlyRepositoryBuilder
{
    public static IWalletWriteOnlyRepository Build()
    {
        var mock = new Mock<IWalletWriteOnlyRepository>();

        return mock.Object;
    }
}

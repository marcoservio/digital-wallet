namespace CommonTestUtilities.Repositories;

public class WalletReadOnlyRepositoryBuilder
{
    private readonly Mock<IWalletReadOnlyRepository> _repository;

    public WalletReadOnlyRepositoryBuilder() => _repository = new Mock<IWalletReadOnlyRepository>();

    public WalletReadOnlyRepositoryBuilder GetByUserId(Wallet? wallet)
    {
        if (wallet is not null)
            _repository.Setup(r => r.GetByUserId(It.IsAny<long>())).ReturnsAsync(wallet);

        return this;
    }

    public WalletReadOnlyRepositoryBuilder GetByWalletKey(Wallet? wallet)
    {
        if (wallet is not null)
            _repository.Setup(r => r.GetByWalletKey(It.IsAny<Guid>())).ReturnsAsync(wallet);

        return this;
    }

    public IWalletReadOnlyRepository Build() => _repository.Object;
}

namespace UseCases.Test.Wallet.Balance.Add;

public class AddBalanceWalletUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var wallet = WalletBuilder.Build(user);
        var request = RequestBalanceJsonBuilder.Build();

        var useCase = CreateUseCase(user, wallet);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Amount_Invalid()
    {
        (var user, _) = UserBuilder.Build();
        var wallet = WalletBuilder.Build(user);
        var request = new RequestBalanceJson { Amount = 0 };

        var useCase = CreateUseCase(user, wallet);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<OnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 &&
                        e.GetErrorMessages().Contains(ResourceMessagesException.AMOUNT_GREATER_THAN_ZERO));
    }

    [Fact]
    public async Task Error_Wallet_Not_Found()
    {
        (var user, _) = UserBuilder.Build();
        var request = RequestBalanceJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<NotFoundException>())
            .Where(e => e.Message.Equals(ResourceMessagesException.WALLET_NOT_FOUND));
    }

    private static AddBalanceWalletUseCase CreateUseCase(DigitalWallet.Domain.Entities.User user, DigitalWallet.Domain.Entities.Wallet? wallet = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var walletReadOnlyRepositoryBuilder = new WalletReadOnlyRepositoryBuilder().GetByUserId(wallet).Build();
        var walletWriteOnlyRepository = WalletWriteOnlyRepositoryBuilder.Build();

        return new AddBalanceWalletUseCase(loggedUser, unitOfWork, walletReadOnlyRepositoryBuilder, walletWriteOnlyRepository);
    }
}

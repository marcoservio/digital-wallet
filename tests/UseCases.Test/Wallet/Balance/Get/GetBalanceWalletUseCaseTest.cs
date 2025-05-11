using DigitalWallet.Application.UseCases.Wallet.Balance.Get;

namespace UseCases.Test.Wallet.Balance.Get;

public class GetBalanceWalletUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var wallet = WalletBuilder.Build(user);
        var request = RequestBalanceJsonBuilder.Build();

        var useCase = CreateUseCase(user, wallet);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.WalletKey.Should().NotBe(Guid.Empty);
        result.Balance.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Error_Wallet_Not_Found()
    {
        (var user, _) = UserBuilder.Build();
        var request = RequestBalanceJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        Func<Task> act = useCase.Execute;

        (await act.Should().ThrowAsync<NotFoundException>())
            .Where(e => e.Message.Equals(ResourceMessagesException.WALLET_NOT_FOUND));
    }

    private static GetBalanceWalletUseCase CreateUseCase(DigitalWallet.Domain.Entities.User user, DigitalWallet.Domain.Entities.Wallet? wallet = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var walletReadOnlyRepositoryBuilder = new WalletReadOnlyRepositoryBuilder().GetByUserId(wallet).Build();
        var mapper = MapperBuilder.Build();

        return new GetBalanceWalletUseCase(loggedUser, walletReadOnlyRepositoryBuilder, mapper);
    }
}

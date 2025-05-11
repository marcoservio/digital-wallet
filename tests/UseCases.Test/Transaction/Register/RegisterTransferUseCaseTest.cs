using DigitalWallet.Domain.Entities;

namespace UseCases.Test.Transaction.Register;

public class RegisterTransferUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var fromWallet = WalletBuilder.Build(user);
        var toWallet = WalletBuilder.Build(id: 2);
        var request = RequestTransferJsonBuilder.Build(toWallet.WalletKey);

        var useCase = CreateUseCase(user, fromWallet, toWallet);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_From_Wallet_Not_Found()
    {
        (var user, _) = UserBuilder.Build();
        var request = RequestTransferJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<NotFoundException>())
            .Where(e => e.Message.Equals(ResourceMessagesException.WALLET_NOT_FOUND));
    }

    [Fact]
    public async Task Error_To_Wallet_Not_Found()
    {
        (var user, _) = UserBuilder.Build();
        var fromWallet = WalletBuilder.Build(user);
        var request = RequestTransferJsonBuilder.Build();

        var useCase = CreateUseCase(user, fromWallet);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<NotFoundException>())
            .Where(e => e.Message.Equals(ResourceMessagesException.WALLET_NOT_FOUND));
    }

    [Fact]
    public async Task Error_Wallet_With_Same_Id()
    {
        (var user, _) = UserBuilder.Build();
        var fromWallet = WalletBuilder.Build(user);
        var toWallet = WalletBuilder.Build();
        var request = RequestTransferJsonBuilder.Build(toWallet.WalletKey);

        var useCase = CreateUseCase(user, fromWallet, toWallet);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<OnValidationException>())
           .Where(e => e.GetErrorMessages().Count == 1 &&
                       e.GetErrorMessages().Contains(ResourceMessagesException.WALLET_SAME));
    }

    [Fact]
    public async Task Error_Amount_Greater_Than_From_Wallet_Balance()
    {
        (var user, _) = UserBuilder.Build();
        var fromWallet = WalletBuilder.Build(user);
        var toWallet = WalletBuilder.Build(id: 2);
        var request = RequestTransferJsonBuilder.Build(toWallet.WalletKey);
        request.Amount = fromWallet.Balance + 1;

        var useCase = CreateUseCase(user, fromWallet, toWallet);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<OnValidationException>())
           .Where(e => e.GetErrorMessages().Count == 1 &&
                       e.GetErrorMessages().Contains(ResourceMessagesException.WALLET_INSUFFICIENT_BALANCE));
    }

    private static RegisterTransferUseCase CreateUseCase(DigitalWallet.Domain.Entities.User user, DigitalWallet.Domain.Entities.Wallet? fromWallet = null, DigitalWallet.Domain.Entities.Wallet? toWallet = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var walletReadOnlyRepository = new WalletReadOnlyRepositoryBuilder().GetByUserId(fromWallet).GetByWalletKey(toWallet).Build();
        var walletWriteOnlyRepository = WalletWriteOnlyRepositoryBuilder.Build();
        var transactionWriteOnlyRepository = TransactionWriteOnlyRepositoryBuilder.Build();

        return new RegisterTransferUseCase(unitOfWork, loggedUser, walletReadOnlyRepository, walletWriteOnlyRepository, transactionWriteOnlyRepository);
    }
}

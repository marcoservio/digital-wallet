using CommonTestUtilities.Cache;

using DigitalWallet.Application.UseCases.Transfer.Filter;

namespace UseCases.Test.Transaction.Filter;

public class FilterTransferUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var toWallet = WalletBuilder.Build(user);
        var fromWallet = WalletBuilder.Build(id: 2);
        var transactions = TransactionBuilder.Collection(fromWallet, toWallet);

        var request = RequestFilterTransferJsonBuilder.Build();

        var useCase = CreateUseCase(user, transactions);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Transfers.Should().NotBeNull();
        result.Transfers.Should().HaveCount(transactions.Count);
    }

    [Fact]
    public async Task Error_StartDate_Greater_Than_EndDate()
    {
        (var user, _) = UserBuilder.Build();
        var toWallet = WalletBuilder.Build(user);
        var fromWallet = WalletBuilder.Build(id: 2);
        var transactions = TransactionBuilder.Collection(fromWallet, toWallet);

        var request = RequestFilterTransferJsonBuilder.Build(true);

        var useCase = CreateUseCase(user, transactions);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<OnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 &&
                        e.GetErrorMessages().Contains(ResourceMessagesException.START_DATE_LESS_THAN_END_DATE));
    }

    private static FilterTransferUseCase CreateUseCase(DigitalWallet.Domain.Entities.User user, IList<DigitalWallet.Domain.Entities.Transaction> transactions, bool withChache = false)
    {
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var transactionReadOnlyRepository = new TransactionReadOnlyRepositoryBuilder().Filter(transactions).Build();
        var cache = new CacheServiceBuilder().GetAsync(transactions, withChache).Build();

        return new FilterTransferUseCase(mapper, loggedUser, transactionReadOnlyRepository, cache);
    }
}

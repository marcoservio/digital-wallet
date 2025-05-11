namespace CommonTestUtilities.Entities;

public class TransactionBuilder
{
    public static IList<Transaction> Collection(Wallet fromWallet, Wallet toWallet, uint count = 2)
    {
        var list = new List<Transaction>();

        if (count == 0)
            count = 1;

        var transactionId = 1;

        for (var i = 0; i < count; i++)
        {
            var fakeTransaction = Build(fromWallet, toWallet);
            fakeTransaction.Id = transactionId++;

            list.Add(fakeTransaction);
        }

        return list;
    }

    public static Transaction Build(Wallet fromWallet, Wallet toWallet)
    {
        return new Faker<Transaction>()
            .RuleFor(r => r.Id, _ => 1)
            .RuleFor(r => r.FromWalletId, _ => fromWallet.Id)
            .RuleFor(r => r.ToWalletId, _ => toWallet.Id)
            .RuleFor(r => r.Amount, _ => fromWallet.Balance / 2)
            .RuleFor(r => r.Description, f => f.Lorem.Sentence(3))
            .RuleFor(r => r.Status, f => f.PickRandom<DigitalWallet.Domain.Enums.TransactionStatus>());
    }
}

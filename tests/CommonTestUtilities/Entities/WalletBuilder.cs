namespace CommonTestUtilities.Entities;

public class WalletBuilder
{
    public static Wallet Build(User? user = null, int id = 1)
    {
        return new Faker<Wallet>()
            .RuleFor(x => x.Id, () => id)
            .RuleFor(x => x.Balance, f => f.Finance.Amount(min: 11))
            .RuleFor(x => x.UserId, _ => user?.Id ?? 1000);
    }
}

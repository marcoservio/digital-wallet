namespace CommonTestUtilities.Requests;

public class RequestBalanceJsonBuilder
{
    public static RequestBalanceJson Build()
    {
        return new Faker<RequestBalanceJson>()
            .RuleFor(x => x.Amount, f => f.Finance.Amount(min: 1));
    }
}

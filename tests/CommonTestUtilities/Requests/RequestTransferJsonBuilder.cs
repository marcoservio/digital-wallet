namespace CommonTestUtilities.Requests;

public class RequestTransferJsonBuilder
{
    public static RequestTransferJson Build(Guid? walletKey = null)
    {
        return new Faker<RequestTransferJson>()
            .RuleFor(x => x.Amount, f => f.Finance.Amount(min: 1, max: 10))
            .RuleFor(x => x.Description, f => f.Lorem.Sentence(3))
            .RuleFor(x => x.ToWalletKey, _ => walletKey ?? Guid.NewGuid());
    }
}

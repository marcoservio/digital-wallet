namespace CommonTestUtilities.Requests;

public class RequestFilterTransferJsonBuilder
{
    public static RequestFilterTransferJson Build(bool forceStartDateGreaterThanEndDate = false, DateTime? transactionDate = null, bool forceDifferentDateFromTransaction = false)
    {
        var faker = new Faker();

        var startDate = transactionDate ?? faker.Date.Past();
        var endDate = startDate.AddDays(5);

        if (forceDifferentDateFromTransaction && startDate == transactionDate)
            startDate = startDate.AddDays(1);

        if (startDate > endDate)
            endDate = startDate.AddDays(5);

        if (forceStartDateGreaterThanEndDate)
            startDate = endDate.AddDays(5);

        return new Faker<RequestFilterTransferJson>()
            .RuleFor(x => x.StartDate, f => startDate)
            .RuleFor(x => x.EndDate, f => endDate);
    }
}

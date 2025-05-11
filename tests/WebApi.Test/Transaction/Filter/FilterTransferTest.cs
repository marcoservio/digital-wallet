namespace WebApi.Test.Transaction.Filter;

public class FilterTransferTest(CustomWebApplicationFactory factory) : DigitalWalletClassFixture(factory)
{
    private readonly string METHOD = "transfers/filter";

    [Fact]
    public async Task Success()
    {
        var request = RequestFilterTransferJsonBuilder.Build(transactionDate: _transactionDate);

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: METHOD, request: request, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("transfers").EnumerateArray().Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Success_NoContent()
    {
        var request = RequestFilterTransferJsonBuilder.Build(transactionDate: _transactionDate, forceDifferentDateFromTransaction: true);

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: METHOD, request: request, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_StartDate_Greater_Than_EndDate(string culture)
    {
        var request = RequestFilterTransferJsonBuilder.Build(forceStartDateGreaterThanEndDate: true);

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: METHOD, request: request, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        (var expectedMessage, var errors, _) = await ErrorHandler.GetErrorMessage(response, culture, "START_DATE_LESS_THAN_END_DATE");

        errors.Should().ContainSingle().And.Contain(errors => errors.GetString()!.Equals(expectedMessage));
    }
}

namespace WebApi.Test.Wallet.Balance.Add;

public class AddBalanceWalletTest(CustomWebApplicationFactory factory) : DigitalWalletClassFixture(factory)
{
    private readonly string METHOD = "wallets/balance";

    [Fact]
    public async Task Success()
    {
        var request = RequestBalanceJsonBuilder.Build();

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: METHOD, request: request, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Amount_Invalid(string culture)
    {
        var request = new RequestBalanceJson { Amount = 0 };

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: METHOD, request: request, culture: culture, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        (var expectedMessage, var errors, _) = await ErrorHandler.GetErrorMessage(response, culture, "AMOUNT_GREATER_THAN_ZERO");

        errors.Should().ContainSingle().And.Contain(errors => errors.GetString()!.Equals(expectedMessage));
    }
}

namespace WebApi.Test.Wallet.Balance.Get;

public class GetBalanceWalletTest(CustomWebApplicationFactory factory) : DigitalWalletClassFixture(factory)
{
    private readonly string METHOD = "wallets/balance";

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet(method: METHOD, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("walletKey").GetGuid().Should().Be(_fromWalletKey);
        responseData.RootElement.GetProperty("balance").GetDecimal().Should().Be(_balance);
    }
}

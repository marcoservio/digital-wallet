namespace WebApi.Test.Transaction.Register;

public class RegisterTransferTest(CustomWebApplicationFactory factory) : DigitalWalletClassFixture(factory)
{
    private readonly string METHOD = "transfers";

    [Fact]
    public async Task Success()
    {
        var request = RequestTransferJsonBuilder.Build(_toWalletKey);

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: METHOD, request: request, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Amount_Invalid(string culture)
    {
        var request = RequestTransferJsonBuilder.Build(_fromWalletKey);
        request.Amount = 0;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: METHOD, request: request, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        (var expectedMessage, var errors, _) = await ErrorHandler.GetErrorMessage(response, culture, "AMOUNT_GREATER_THAN_ZERO");

        errors.Should().ContainSingle().And.Contain(errors => errors.GetString()!.Equals(expectedMessage));
    }
}

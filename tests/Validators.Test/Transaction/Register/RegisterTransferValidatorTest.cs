namespace Validators.Test.Transaction.Register;

public class RegisterTransferValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestTransferJsonBuilder.Build();

        var result = new RegisterTransferValidator().Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    public void Error_Amount_Invalid(decimal amount)
    {
        var request = RequestTransferJsonBuilder.Build();
        request.Amount = amount;

        var result = new RegisterTransferValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Be(ResourceMessagesException.AMOUNT_GREATER_THAN_ZERO);
    }

    [Fact]
    public void Error_WalletKey_Empty()
    {
        var request = RequestTransferJsonBuilder.Build();
        request.ToWalletKey = Guid.Empty;

        var result = new RegisterTransferValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Be(ResourceMessagesException.WALLET_KEY_EMPTY);
    }

    [Fact]
    public void Error_Description_Too_Long()
    {
        var request = RequestTransferJsonBuilder.Build();
        request.Description = RequestStringGenerator.Paragraphs(minCharacteres: 2001);

        var result = new RegisterTransferValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Be(ResourceMessagesException.DESCRIPTION_EXCEEDS_LIMIT_CHARACTERS);
    }
}

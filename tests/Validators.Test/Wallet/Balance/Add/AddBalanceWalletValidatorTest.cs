namespace Validators.Test.Wallet.Balance.Add;

public class AddBalanceWalletValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestBalanceJsonBuilder.Build();

        var result = new AddBalanceWalletValidator().Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    public void Error_Amount_Invalid(decimal amount)
    {
        var request = new RequestBalanceJson { Amount = amount };

        var result = new AddBalanceWalletValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Be(ResourceMessagesException.AMOUNT_GREATER_THAN_ZERO);
    }
}

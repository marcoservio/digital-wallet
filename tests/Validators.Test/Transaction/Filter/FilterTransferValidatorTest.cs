namespace Validators.Test.Transaction.Filter;

public class FilterTransferValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestFilterTransferJsonBuilder.Build();

        var result = new FilterTransferValidator().Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_StartDate_Greater_Than_EndDate()
    {
        var request = RequestFilterTransferJsonBuilder.Build(forceStartDateGreaterThanEndDate: true);

        var result = new FilterTransferValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Be(ResourceMessagesException.START_DATE_LESS_THAN_END_DATE);
    }
}

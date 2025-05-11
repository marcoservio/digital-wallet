namespace DigitalWallet.Application.UseCases.Transfer.Filter;

public class FilterTransferValidator : AbstractValidator<RequestFilterTransferJson>
{
    public FilterTransferValidator()
    {
        RuleFor(user => user.StartDate)
            .LessThan(user => user.EndDate)
            .WithMessage(ResourceMessagesException.START_DATE_LESS_THAN_END_DATE);
    }
}

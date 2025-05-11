namespace DigitalWallet.Application.UseCases.Wallet.Balance.Add;

public class AddBalanceWalletValidator : AbstractValidator<RequestBalanceJson>
{
    public AddBalanceWalletValidator()
    {
        RuleFor(user => user.Amount).GreaterThan(0).WithMessage(ResourceMessagesException.AMOUNT_GREATER_THAN_ZERO);
    }
}

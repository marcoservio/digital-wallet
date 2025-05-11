namespace DigitalWallet.Application.UseCases.Transfer.Register;

public class RegisterTransferValidator : AbstractValidator<RequestTransferJson>
{
    public RegisterTransferValidator()
    {
        RuleFor(user => user.Amount).GreaterThan(0).WithMessage(ResourceMessagesException.AMOUNT_GREATER_THAN_ZERO);
        RuleFor(user => user.Description).MaximumLength(255).WithMessage(ResourceMessagesException.DESCRIPTION_EXCEEDS_LIMIT_CHARACTERS);
        RuleFor(user => user.ToWalletKey).NotEmpty().WithMessage(ResourceMessagesException.WALLET_KEY_EMPTY);
    }
}

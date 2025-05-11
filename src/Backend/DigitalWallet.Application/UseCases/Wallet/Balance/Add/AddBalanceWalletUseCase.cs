namespace DigitalWallet.Application.UseCases.Wallet.Balance.Add;

public class AddBalanceWalletUseCase(
    ILoggedUser loggedUser, IUnitOfWork unitOfWork,
    IWalletReadOnlyRepository walletReadOnlyRepository,
    IWalletWriteOnlyRepository walletWriteOnlyRepository) : IAddBalanceWalletUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IWalletReadOnlyRepository _walletReadOnlyRepository = walletReadOnlyRepository;
    private readonly IWalletWriteOnlyRepository _walletWriteOnlyRepository = walletWriteOnlyRepository;

    public async Task Execute(RequestBalanceJson request)
    {
        Validate(request);

        var authenticatedUser = await _loggedUser.User();

        var wallet = await _walletReadOnlyRepository.GetByUserId(authenticatedUser.Id) ?? throw new NotFoundException(ResourceMessagesException.WALLET_NOT_FOUND);

        wallet.Balance += request.Amount;
        wallet.UpdatedAt = DateTime.UtcNow;

        _walletWriteOnlyRepository.Update(wallet);

        await _unitOfWork.Commit();
    }

    private static void Validate(RequestBalanceJson request)
    {
        var result = new AddBalanceWalletValidator().Validate(request);

        if (result.IsValid.IsFalse())
            throw new OnValidationException([.. result.Errors.Select(e => e.ErrorMessage)]);
    }
}

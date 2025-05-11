namespace DigitalWallet.Application.UseCases.Transfer.Register;

public class RegisterTransferUseCase(IUnitOfWork unitOfWork, ILoggedUser loggedUser,
    IWalletReadOnlyRepository walletReadOnlyRepository, IWalletWriteOnlyRepository walletWriteOnlyRepository,
    ITransactionWriteOnlyRepository transactionWriteOnlyRepository) : IRegisterTransferUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IWalletReadOnlyRepository _walletReadOnlyRepository = walletReadOnlyRepository;
    private readonly IWalletWriteOnlyRepository _walletWriteOnlyRepository = walletWriteOnlyRepository;
    private readonly ITransactionWriteOnlyRepository _transactionWriteOnlyRepository = transactionWriteOnlyRepository;

    public async Task Execute(RequestTransferJson request)
    {
        Validate(request);

        var authenticatedUser = await _loggedUser.User();

        var fromWallet = await _walletReadOnlyRepository.GetByUserId(authenticatedUser.Id)
            ?? throw new NotFoundException(ResourceMessagesException.WALLET_NOT_FOUND);

        var toWallet = await _walletReadOnlyRepository.GetByWalletKey(request.ToWalletKey)
            ?? throw new NotFoundException(ResourceMessagesException.WALLET_NOT_FOUND);

        if (fromWallet.Id == toWallet.Id)
            throw new OnValidationException([ResourceMessagesException.WALLET_SAME]);

        if (fromWallet.Balance < request.Amount)
            throw new OnValidationException([ResourceMessagesException.WALLET_INSUFFICIENT_BALANCE]);

        var transfer = new Transaction
        {
            Amount = request.Amount,
            Description = request.Description,
            FromWalletId = fromWallet.Id,
            ToWalletId = toWallet.Id,
            Status = Domain.Enums.TransactionStatus.Pending,
        };

        await _transactionWriteOnlyRepository.Add(transfer);
        await _unitOfWork.Commit();

        try
        {
            fromWallet.Balance -= request.Amount;
            fromWallet.UpdatedAt = DateTime.UtcNow;

            toWallet.Balance += request.Amount;
            toWallet.UpdatedAt = DateTime.UtcNow;

            _walletWriteOnlyRepository.Update(fromWallet);
            _walletWriteOnlyRepository.Update(toWallet);

            transfer.Status = Domain.Enums.TransactionStatus.Completed;
            transfer.UpdatedAt = DateTime.UtcNow;

            _transactionWriteOnlyRepository.Update(transfer);

            await _unitOfWork.Commit();
        }
        catch
        {
            transfer.Status = Domain.Enums.TransactionStatus.Failed;
            transfer.UpdatedAt = DateTime.UtcNow;

            _transactionWriteOnlyRepository.Update(transfer);

            await _unitOfWork.Commit();

            throw;
        }
    }

    private static void Validate(RequestTransferJson request)
    {
        var result = new RegisterTransferValidator().Validate(request);

        if (result.IsValid.IsFalse())
            throw new OnValidationException([.. result.Errors.Select(e => e.ErrorMessage)]);
    }
}

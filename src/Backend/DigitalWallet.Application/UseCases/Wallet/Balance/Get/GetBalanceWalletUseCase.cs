namespace DigitalWallet.Application.UseCases.Wallet.Balance.Get;

public class GetBalanceWalletUseCase(ILoggedUser loggedUser, IWalletReadOnlyRepository walletReadOnlyRepository, IMapper mapper) : IGetBalanceWalletUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IWalletReadOnlyRepository _walletReadOnlyRepository = walletReadOnlyRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseBalanceJson> Execute()
    {
        var authenticatedUser = await _loggedUser.User();

        var wallet = await _walletReadOnlyRepository.GetByUserId(authenticatedUser.Id) ?? throw new NotFoundException(ResourceMessagesException.WALLET_NOT_FOUND);

        return _mapper.Map<ResponseBalanceJson>(wallet);
    }
}

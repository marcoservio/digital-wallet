namespace DigitalWallet.Application.UseCases.Wallet.Balance.Get;

public interface IGetBalanceWalletUseCase
{
    Task<ResponseBalanceJson> Execute();
}

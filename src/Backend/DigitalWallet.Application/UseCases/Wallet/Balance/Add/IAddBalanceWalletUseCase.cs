namespace DigitalWallet.Application.UseCases.Wallet.Balance.Add;

public interface IAddBalanceWalletUseCase
{
    Task Execute(RequestBalanceJson request);
}

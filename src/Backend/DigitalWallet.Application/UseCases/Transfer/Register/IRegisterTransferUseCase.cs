namespace DigitalWallet.Application.UseCases.Transfer.Register;

public interface IRegisterTransferUseCase
{
    Task Execute(RequestTransferJson request);
}

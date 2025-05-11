namespace DigitalWallet.Application.UseCases.Transfer.Filter;

public interface IFilterTransferUseCase
{
    Task<ResponseTransfersJson> Execute(RequestFilterTransferJson request);
}

namespace DigitalWallet.Api.Controllers;

[AuthenticatedUser]
public class TransfersController : DigitalWalletBaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromServices] IRegisterTransferUseCase useCase, [FromBody] RequestTransferJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpPost("filter")]
    [ProducesResponseType(typeof(ResponseTransfersJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Filter([FromServices] IFilterTransferUseCase useCase, [FromBody] RequestFilterTransferJson request)
    {
        var response = await useCase.Execute(request);

        if (response.Transfers.Any())
            return Ok(response);

        return NoContent();
    }
}

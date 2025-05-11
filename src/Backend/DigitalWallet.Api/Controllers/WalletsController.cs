namespace DigitalWallet.Api.Controllers;

[AuthenticatedUser]
public class WalletsController : DigitalWalletBaseController
{
    [HttpGet("balance")]
    [ProducesResponseType(typeof(ResponseBalanceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBalance([FromServices] IGetBalanceWalletUseCase useCase)
    {
        var balance = await useCase.Execute();

        return Ok(balance);
    }

    [HttpPost("balance")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddBalance([FromServices] IAddBalanceWalletUseCase useCase, [FromBody] RequestBalanceJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }
}

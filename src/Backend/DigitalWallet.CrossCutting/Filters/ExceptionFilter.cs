namespace DigitalWallet.CrossCutting.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DigitalWalletException digitalWalletException)
            HandleProjectException(digitalWalletException, context);
        else if (context.Exception is DbUpdateConcurrencyException dbUpdateException)
            HandleDbUpdateException(dbUpdateException, context);
        else
            ThrowUnknowException(context);

        context.ExceptionHandled = true;
    }

    private static void HandleProjectException(DigitalWalletException digitalWalletException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)digitalWalletException.GetStatusCode();
        context.Result = new ObjectResult(new ResponseErrorJson(digitalWalletException.GetErrorMessages()));
    }

    private static void HandleDbUpdateException(DbUpdateConcurrencyException dbUpdateException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.WALLET_MODIFIED_BY_ANOTHER_PROCESS));
    }

    private static void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
    }
}

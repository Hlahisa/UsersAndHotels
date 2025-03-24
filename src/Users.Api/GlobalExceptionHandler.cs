namespace Users.Api;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken _)
    {
        httpContext.Response.ContentType = Constants.Web.JsonContent;
        
        SetResponseStatusCode(httpContext, exception);

        logger.LogError("Something went wrong: {exception}", exception.Message);

        await WriteErrorResponseAsync(httpContext, exception);

        return true;
    }

    private static void SetResponseStatusCode(
        HttpContext httpContext,
        Exception exception)
    {
        httpContext.Response.StatusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static async Task WriteErrorResponseAsync(
        HttpContext httpContext, 
        Exception exception)
    {
        await httpContext.Response.WriteAsync(GetErrorDetails().ToString());
        
        return;
        
        ErrorDetails GetErrorDetails() => new (exception.Message);
    }
}
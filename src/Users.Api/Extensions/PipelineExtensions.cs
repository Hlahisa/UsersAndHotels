namespace Users.Api.Extensions;

public static class PipelineExtensions
{
    private const string ApiDescription = "Users API";

    public static WebApplication ConfigurePipeline(this WebApplication webApp)
    {
        webApp.ConfigureExceptionHandling();

        webApp.UseSwagger();
        webApp.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(Endpoints.Documentation.Url, ApiDescription);
        });

        webApp.UseHsts();
        webApp.UseHttpsRedirection();
        webApp.MapControllers();

        return webApp;
    }

    public static void ConfigureExceptionHandling(this WebApplication webApp)
    {
        webApp.UseExceptionHandler(new ExceptionHandlerOptions
        {
            ExceptionHandler = async (HttpContext httpContext) =>
            {
                var exceptionHandler = httpContext.RequestServices.GetRequiredService<GlobalExceptionHandler>();

                var exceptionFeature = httpContext.Features.Get<IExceptionHandlerFeature>()!;
                
                if (exceptionFeature.Error is { } exception)
                {
                    await exceptionHandler.TryHandleAsync(httpContext, exception, CancellationToken.None);
                }
            }
        });
    }
}
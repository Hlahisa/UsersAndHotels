
WebApplicationBuilder webAppBuilder;

try
{
    webAppBuilder = WebApplication.CreateBuilder(args).RegisterServices();
}
catch (Exception serviceRegistrationException)
{
    Console.WriteLine(serviceRegistrationException);
    
    throw;
}

try
{
    WebApplication webApp = webAppBuilder.Build().ConfigurePipeline();

    webApp.Run();
}
catch (Exception appExecutionException)
{
    Console.WriteLine(appExecutionException);
    
    throw;
}
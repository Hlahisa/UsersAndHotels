namespace Users.Api.Extensions;

public static class ServiceExtensions
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder webAppBuilder)
    {
        webAppBuilder.ConfigureSerilogLogging();
        webAppBuilder.ConfigureUserSettings();

        webAppBuilder.Services.RegisterStandardApplicationServices();
        webAppBuilder.Services.RegisterCustomApplicationServices(webAppBuilder.Configuration);

        return webAppBuilder;
    }

    public static void ConfigureSerilogLogging(this WebApplicationBuilder webAppBuilder)
    {
        webAppBuilder.Host.UseSerilog((HostBuilderContext ctx, LoggerConfiguration cfg) =>
            cfg.ReadFrom.Configuration(ctx.Configuration));
    }

    public static void ConfigureUserSettings(this WebApplicationBuilder webAppBuilder)
    {
        webAppBuilder.Services.Configure<UserSettings>(
            webAppBuilder.Configuration.GetSection(UserSettings.ConfigSection));
    }

    public static void RegisterStandardApplicationServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
    }
    
    public static void RegisterCustomApplicationServices(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddHttpClient<IUserRepository, InMemoryUserRepository>(
            httpClient => httpClient.BaseAddress = new Uri(GetUserSettings(configuration).BaseUri));
        
        services.AddScoped<IGeoService, GeoService>();
        services.AddScoped<IUserService, UserService>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddAutoMapper(typeof(IApiAssemblyMarker));
        
        return;

        static UserSettings GetUserSettings(ConfigurationManager config)
            => config.GetSection(UserSettings.ConfigSection).Get<UserSettings>()!;
    }
}
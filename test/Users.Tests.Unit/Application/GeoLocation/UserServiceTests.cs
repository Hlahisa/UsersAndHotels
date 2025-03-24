namespace Users.Tests.Unit.Application.GeoLocation;

public sealed class UserServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockMessageHandler;
    private readonly HttpClient _testClient;

    private const string TargetExecutionMethod = "SendAsync";
    private const string TestUsersBaseUrl = "https://test.com";
    private const string TestUsersUrl = "/users";

    public UserServiceTests()
    {
        _mockMessageHandler = new Mock<HttpMessageHandler>();
        _testClient = new HttpClient(_mockMessageHandler.Object);
    }

    [Theory]
    [InlineData(-43.9509, -34.4618, 2)]
    [InlineData(40.7128, -74.0060, 8)]
    [InlineData(34.0522, -118.2437, 4)]
    [InlineData(-25.2744, 133.7751, 1)]
    public async Task GetNearestUserToHotelAsync_HotelCoordinatesSupplied_NearestUserReturned(
        double hotelLat, 
        double hotelLng, 
        int expectedNearestUserId)
    {
        // Arrange
        ConfigureUserDataRetrieval();
        ConfigureHttpUserDataUrl();
        var mapperConfig = GetUserDataMapperConfiguration();
        var userService = GetUserService(mapperConfig);

        // Act
        var nearestUser = await userService.GetNearestUserToHotelAsync(
            GetHotelCoordinates(hotelLat, hotelLng));

        // Assert
        Assert.True(nearestUser!.Id == expectedNearestUserId);
    }

    private void ConfigureUserDataRetrieval()
    {
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                TargetExecutionMethod,
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    TestData.GetUserData(),
                    Encoding.UTF8,
                    Constants.Web.JsonContent)
            });
    }

    private void ConfigureHttpUserDataUrl() => _testClient.BaseAddress = new Uri(TestUsersBaseUrl);

    private static MapperConfiguration GetUserDataMapperConfiguration()
    {
        return new((IMapperConfigurationExpression configure) =>
        {
            configure.AddProfile<MappingProfile>();
        });
    }

    private UserService GetUserService(MapperConfiguration mapperConfig)
    {
        var userService = new UserService(
            new InMemoryUserRepository(
                _testClient,
                new OptionsWrapper<UserSettings>(GetUserSettings())),
            new GeoService(),
            mapperConfig.CreateMapper());
        return userService;
    }

    private static UserSettings GetUserSettings()
    {
        return new()
        {
            BaseUri = TestUsersBaseUrl, 
            UsersUri = TestUsersUrl
        };
    }

    private static HotelCoordinates GetHotelCoordinates(double hotelLat, double hotelLng)
    {
        return new()
        {
            Lat = hotelLat,
            Lng = hotelLng
        };
    }
}
namespace Users.Api;

public static class Endpoints
{
    private const string ServiceType = "api";
    
    public static class Users
    {
        public const string Base = $"{ServiceType}/users";

        public static class Read
        {
            public const string NearestHotelUser = "nearest-hotel-user";
        }
    }

    public static class Documentation
    {
        public const string Url = "/swagger/v1/swagger.json";
    }
}
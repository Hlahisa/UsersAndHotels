namespace Users.Api;

public sealed class MappingProfile : Profile
{
	public MappingProfile()
	{
        MapInternalToExternalUserResponse();

        MapUpdateToInternalUserRequest();
    }

    private void MapInternalToExternalUserResponse()
    {
        CreateMap<User, UserResponse>()
            .ForMember(userResponse => userResponse.FullAddress,
                opts => opts.MapFrom(user => GetUserAddress(user)))
            .ForMember(userResponse => userResponse.GeoLocation,
                opts => opts.MapFrom(user => GetUserGeoLocation(user)))
            .ForMember(userResponse => userResponse.CompanyDetails,
                opts => opts.MapFrom(user => GetUserCompanyDetails(user)));
    }
    
    private static string GetUserAddress(User user) => 
        string.Join(", ", user.Address.Street, user.Address.Suite, user.Address.City, user.Address.Zipcode);

    private static string GetUserGeoLocation(User user) => 
        $"Latitude: {user.Address.Geo.Lat}; Longitude: {user.Address.Geo.Lng}";

    private static string GetUserCompanyDetails(User user) => 
        string.Join(", ", user.Company.Name, user.Company.CatchPhrase, user.Company.Bs);

    private void MapUpdateToInternalUserRequest()
    {
        CreateMap<UserUpdateRequest, User>()
            .ForMember(user => user.Address,
                opts => opts.MapFrom(user => GetInternalUserAddress(user)))
            .ForMember(user => user.Company,
                opts => opts.MapFrom(user => GetInternalUserCompany(user)));
    }

    private static UserAddress GetInternalUserAddress(UserUpdateRequest user)
    {
        var addressDetails = user.FullAddress.Split(", ");
        var geoDetails = user.GeoLocation.Split("; ");
        
        return new()
        {
            Street = addressDetails[0],
            Suite = addressDetails[1],
            City = addressDetails[2],
            Zipcode = addressDetails[3],
            Geo = new UserGeo
            {
                Lat = geoDetails[0].Split(" ")[^1],
                Lng = geoDetails[1].Split(" ")[^1]
            }
        };
    }

    private static UserCompany GetInternalUserCompany(UserUpdateRequest user)
    {
        var companyDetails = user.FullAddress.Split(", ");

        return new()
        {
            Name = companyDetails[0],
            CatchPhrase = companyDetails[1],
            Bs = companyDetails[2]
        };
    }
}
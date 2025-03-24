namespace Users.Domain.GeoLocation;

public interface IGeoService
{
    User? FindNearestUser(HotelCoordinates coordinates, IEnumerable<User> users);
}
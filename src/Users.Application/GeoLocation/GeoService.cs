namespace Users.Application.GeoLocation;

public sealed class GeoService : IGeoService
{
    private const double EarthRadiusKm = 6371.0;
     
    public User? FindNearestUser(HotelCoordinates coordinates, IEnumerable<User> users)
    {
        return users
            .OrderBy(user => GetDistance(
                coordinates, 
                double.Parse(user.Address.Geo.Lat, CultureInfo.InvariantCulture),
                double.Parse(user.Address.Geo.Lng, CultureInfo.InvariantCulture)))
            .FirstOrDefault();
    }

    private static double GetDistance(HotelCoordinates coordinates, double userLat, double userLng)
    {
        var latDiff = Math.PI / 180 * (userLat - coordinates.Lat);
        var lngDiff = Math.PI / 180 * (userLng - coordinates.Lng);

        var intermediateCalc =
            Math.Sin(latDiff / 2) * Math.Sin(latDiff / 2) +
            Math.Cos(Math.PI / 180 * coordinates.Lat) * Math.Cos(Math.PI / 180 * userLat) *
            Math.Sin(lngDiff / 2) * Math.Sin(lngDiff / 2);

        return EarthRadiusKm * (2 * Math.Atan2(Math.Sqrt(intermediateCalc), Math.Sqrt(1 - intermediateCalc)));
    }
}
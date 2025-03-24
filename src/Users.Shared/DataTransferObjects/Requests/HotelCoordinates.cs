namespace Users.Shared.DataTransferObjects.Requests;

public sealed record HotelCoordinates
{
    public required double Lat { get; init; }
    public required double Lng { get; init; }
}
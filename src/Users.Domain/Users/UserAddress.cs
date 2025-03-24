namespace Users.Domain.Users;

public sealed class UserAddress
{
    public required string Street { get; init; }
    public required string Suite { get; init; }
    public required string City { get; init; }
    public required string Zipcode { get; init; }
    public required UserGeo Geo { get; init; }
}
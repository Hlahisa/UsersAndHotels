namespace Users.Shared.DataTransferObjects.Responses;

public sealed class UserResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string FullAddress { get; init; }
    public required string GeoLocation { get; init; }
    public required string Phone { get; init; }
    public required string Website { get; init; }
    public required string CompanyDetails { get; init; }
}
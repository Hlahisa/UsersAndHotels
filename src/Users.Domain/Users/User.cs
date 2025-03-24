namespace Users.Domain.Users;

public sealed record User : Entity
{
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required UserAddress Address { get; init; }
    public required string Phone { get; init; }
    public required string Website { get; init; }
    public required UserCompany Company { get; init; }
}
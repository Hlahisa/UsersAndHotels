namespace Users.Domain.Users;

public sealed record UserCompany
{
    public required string Name { get; init; }
    public required string CatchPhrase { get; init; }
    public required string Bs { get; init; }
}
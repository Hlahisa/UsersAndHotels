namespace Users.Infrastructure.Users;

public sealed class UserSettings
{
    public const string ConfigSection = "UserSettings";

    public required string BaseUri { get; init; }
    public required string UsersUri { get; init; }
}
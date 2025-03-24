namespace Users.Domain.ErrorModel;

public sealed record ErrorDetails(string? Message)
{
    public override string ToString() => JsonSerializer.Serialize(this);
}
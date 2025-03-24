namespace Users.Domain.Abstractions;

public abstract record Entity
{
    public int Id { get; init; }
}
namespace Users.Domain.Exceptions;

public sealed class UserNotFoundException(int id) : 
    NotFoundException($"User with id '{id}' doesn't exist");
namespace Users.Domain.Exceptions;

public sealed class UserIdMismatchException(int id, int payloadId) :
    BadRequestException($"The given user ids ('{id}' != '{payloadId}') don't match");
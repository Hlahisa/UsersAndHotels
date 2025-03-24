namespace Users.Api.Documentation;

public sealed class AllUsersStatus200ResponseAttribute() :
    ProducesResponseTypeAttribute(
        typeof(IEnumerable<UserResponse>), 
        StatusCodes.Status200OK, 
        Constants.Web.JsonContent);

public sealed class UserStatus200ResponseAttribute() :
    ProducesResponseTypeAttribute(
        typeof(UserResponse),
        StatusCodes.Status200OK,
        Constants.Web.JsonContent);

public sealed class UserStatus204ResponseAttribute() :
    ProducesResponseTypeAttribute(StatusCodes.Status204NoContent);

public sealed class UserStatus404ResponseAttribute() :
    ProducesResponseTypeAttribute(StatusCodes.Status404NotFound);

public sealed class UserStatus500ResponseAttribute() :
    ProducesResponseTypeAttribute(
        typeof(ErrorDetails), 
        StatusCodes.Status500InternalServerError, 
        Constants.Web.JsonContent);
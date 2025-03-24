namespace Users.Api.Controllers;

[ApiController, Route(Endpoints.Users.Base), UserStatus500Response]
public sealed class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet, AllUsersStatus200Response]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await userService.GetUsersAsync());
    }

    [HttpGet("{id:int}"), UserStatus200Response, UserStatus404Response]
    public async Task<IActionResult> GetUser(int id)
    {
        return Ok(await userService.GetUserByIdAsync(id));
    }
    
    [HttpGet(Endpoints.Users.Read.NearestHotelUser), UserStatus200Response, UserStatus500Response]
    public async Task<IActionResult> GetNearestUser([FromQuery] HotelCoordinates coordinates)
    {
        return Ok(await userService.GetNearestUserToHotelAsync(coordinates));
    }

    [HttpPut("{id:int}"), UserStatus204Response, UserStatus404Response]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateRequest user)
    {
        await userService.UpdateUserAsync(id, user);

        return NoContent();
    }

    [HttpDelete("{id:int}"), UserStatus204Response, UserStatus404Response]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userService.DeleteUserAsync(id);

        return NoContent();
    }
}
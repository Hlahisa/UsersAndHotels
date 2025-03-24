namespace Users.Domain.Users;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetUsersAsync();
    Task<UserResponse> GetUserByIdAsync(int id);
    Task<User?> UpdateUserAsync(int id, UserUpdateRequest user);
    Task DeleteUserAsync(int id);
    Task<UserResponse?> GetNearestUserToHotelAsync(HotelCoordinates coordinates);
}
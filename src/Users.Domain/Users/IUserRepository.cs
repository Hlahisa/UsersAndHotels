namespace Users.Domain.Users;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> UpdateUserAsync(User userChanges);
    Task<bool> DeleteUserAsync(int id);
}
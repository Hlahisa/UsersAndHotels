namespace Users.Infrastructure.Users;

public sealed class InMemoryUserRepository(
    HttpClient httpClient,
    IOptions<UserSettings> userSettings) : IUserRepository
{
    private readonly UserSettings _userSettings = userSettings.Value;
    private static readonly List<User> Users = [];

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        await LoadAndCacheUsersAsync();

        return Users;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        await LoadAndCacheUsersAsync();

        return Users.FirstOrDefault(user => user.Id == id);
    }

    public async Task<User?> UpdateUserAsync(User userChanges)
    {
        var index = Users.FindIndex(user => user.Id == userChanges.Id);
        if (index >= 0)
        {
            Users[index] = userChanges;
        }
        
        return await Task.FromResult(userChanges);
    }


    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await GetUserByIdAsync(id);
        if (user is null)
        {
            return false;
        }
            
        Users.Remove(user);
        return true;
    }

    private async Task LoadAndCacheUsersAsync()
    {
        if (Users is [])
        {
            var users = await httpClient.GetFromJsonAsync<List<User>>(_userSettings.UsersUri);
            Users.AddRange(users ?? []);
        }
    }
}
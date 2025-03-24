namespace Users.Application.Users;

public sealed class UserService(
    IUserRepository userRepo,
    IGeoService geoService,
    IMapper mapper) : IUserService
{
    public async Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
        var userEntities = await userRepo.GetAllUsersAsync();
        
        return mapper.Map<IEnumerable<UserResponse>>(userEntities);
    }

    public async Task<UserResponse> GetUserByIdAsync(int id)
    {
        var userEntity = await userRepo.GetUserByIdAsync(id);

        if (userEntity is null)
        {
            throw new UserNotFoundException(id);
        }

        return mapper.Map<UserResponse>(userEntity);
    }

    public async Task<User?> UpdateUserAsync(int id, UserUpdateRequest user)
    {
        if (id != user.Id)
        {
            throw new UserIdMismatchException(id, user.Id);
        }

        var existingUserEntity = await userRepo.GetUserByIdAsync(user.Id);
        if (existingUserEntity is null)
        {
            throw new UserNotFoundException(user.Id);
        }

        var userUpdateEntity = mapper.Map<User>(user);

        return await userRepo.UpdateUserAsync(userUpdateEntity);
    }

    public async Task DeleteUserAsync(int id)
    {
        var userRemoved = await userRepo.DeleteUserAsync(id);

        if (!userRemoved)
        {
            throw new UserNotFoundException(id);
        }
    }

    public async Task<UserResponse?> GetNearestUserToHotelAsync(HotelCoordinates coordinates)
    {
        var userEntities = await userRepo.GetAllUsersAsync();

        var nearestUserEntity = geoService.FindNearestUser(coordinates, userEntities);
        
        return mapper.Map<UserResponse>(nearestUserEntity);
    }
}
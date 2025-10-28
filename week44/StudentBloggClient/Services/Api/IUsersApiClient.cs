using StudentBloggClient.Models.Users;

namespace StudentBloggClient.Services.Api;

public interface IUsersApiClient
{
    Task<IReadOnlyList<UserDto>> GetUsersAsync(int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);
    Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken ct = default);
    Task<UserDto?> RegisterUserAsync(UserRegistrationDto registrationDto, CancellationToken ct = default);
    Task<UserDto?> UpdateUserAsync(Guid id,UserDto userDto, CancellationToken ct = default);
    Task<UserDto?> DeleteUserAsync(Guid id, CancellationToken ct = default);
}
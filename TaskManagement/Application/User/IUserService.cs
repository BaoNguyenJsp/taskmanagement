using TaskManagement.Infrastructure.Models;

namespace TaskManagement.Application.Services
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> GetUsersAsync(QueryParameters queryParameters);

        Task<UserDto> GetUserByIdAsync(int id);

        Task AddUserAsync(UserDto user);

        Task UpdateUserAsync(UserDto user);

        Task DeleteUserAsync(int id);
    }
}
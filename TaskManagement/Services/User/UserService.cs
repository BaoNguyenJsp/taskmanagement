using TaskManagement.Application;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Models;

namespace TaskManagement.Services
{
    public class UserService : IUserService
    {
        public UserService(
            IBaseRepository<User> userRepository
            )
        {
            UserRepository = userRepository;
        }

        public IBaseRepository<User> UserRepository { get; }

        public async Task AddUserAsync(UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email
            };

            await UserRepository.AddAsync(newUser);
            await UserRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid user ID", nameof(id));
            }

            await UserRepository.DeleteAsync(id);
            await UserRepository.SaveChangesAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid user ID", nameof(id));
            }

            return await UserRepository.GetByIdAsync(id)
                .ContinueWith(task =>
                {
                    var user = task.Result;
                    return new UserDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    };
                });
    
        }

        public async Task<PagedResult<UserDto>> GetUsersAsync(QueryParameters queryParameters)
        {
            var users = await UserRepository.GetAllAsync(queryParameters);
            return new PagedResult<UserDto>
            {
                PageNumber = users.PageNumber,
                PageSize = users.PageSize,
                TotalCount = users.TotalCount,
                Items = users.Items.Select(user => new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                }).ToList()
            };
        }

        public async Task UpdateUserAsync(UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Id <= 0)
            {
                throw new ArgumentException("Invalid user ID", nameof(user.Id));
            }
            
            var existingUser = new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            await UserRepository.UpdateAsync(existingUser);
            await UserRepository.SaveChangesAsync();
        }
    }
}

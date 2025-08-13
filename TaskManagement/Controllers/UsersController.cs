using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Models;

namespace TaskManagement.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public IUserService Service { get; }

        public UsersController(IUserService service)
        {
            Service = service;
        }

        // POST: api/users
        [HttpPost("search")]
        public async Task<PagedResult<UserDto>> Get(QueryParameters queryParameters)
        {
            queryParameters ??= new QueryParameters();
            return await Service.GetUsersAsync(queryParameters);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<UserDto> Get(int id)
        {
            if (id <= 0)
            {
                return null; // or throw an exception
            }

            var user = await Service.GetUserByIdAsync(id);
            if (user == null)
            {
                return null; // or throw an exception
            }
            return user;
        }

        // POST: api/users
        [HttpPost]
        public async Task<UserDto> Post([FromBody] UserDto user)
        {
            if (user == null)
            {
                return null; // or throw an exception
            }

            await Service.AddUserAsync(user);
            return user; // or return a created response with the user
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<UserDto> Put(int id, [FromBody] UserDto user)
        {
            if (user == null || user.Id != id)
            {
                return null; // or throw an exception
            }

            var existingUser = await Service.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return null; // or throw an exception
            }

            await Service.UpdateUserAsync(user);
            return user; // or return a response indicating success
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
            {
                return false; // or throw an exception
            }

            var existingUser = await Service.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return false; // or throw an exception
            }

            await Service.DeleteUserAsync(id);
            return true; // or return a response indicating success
        }
    }
}

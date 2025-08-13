using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Models;

namespace TaskManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        public ProjectsController(IProjectService service)
        {
            Service = service;
        }

        public IProjectService Service { get; }

        // POST: api/projects
        [HttpPost("search")]
        public async Task<PagedResult<ProjectDto>> Get(QueryParameters queryParameters)
        {
            await Task.Delay(5000);
            queryParameters ??= new QueryParameters();
            return await Service.GetProjectsAsync(queryParameters);
        }

        // GET: api/projects/5
        [HttpGet("{id}")]
        public async Task<ProjectDto> Get(int id)
        {
            if (id <= 0)
            {
                return null; // or throw an exception
            }

            var project = await Service.GetProjectByIdAsync(id);
            if (project == null)
            {
                return null; // or throw an exception
            }
            return project;
        }

        // POST: api/projects
        [HttpPost]
        public async Task<ProjectDto> Post([FromBody] ProjectDto project)
        {
            if (project == null)
            {
                return null; // or throw an exception
            }

            await Service.AddProjectAsync(project);
            return project; // or return a created response with the project
        }

        // PUT: api/projects/5
        [HttpPut("{id}")]
        public async Task<ProjectDto> Put(int id, [FromBody] ProjectDto project)
        {
            if (project == null || project.Id != id)
            {
                return null; // or throw an exception
            }

            var existingProject = await Service.GetProjectByIdAsync(id);
            if (existingProject == null)
            {
                return null; // or throw an exception
            }

            await Service.UpdateProjectAsync(project);
            return project; // or return a response indicating success
        }

        // DELETE: api/projects/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
            {
                return false; // or throw an exception
            }

            await Service.DeleteProjectAsync(id);
            return true; // or return a response indicating success
        }
    }
}

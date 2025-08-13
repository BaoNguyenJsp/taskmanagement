using TaskManagement.Infrastructure.Models;

namespace TaskManagement.Application.Services
{
    public interface IProjectService
    {
        Task<PagedResult<ProjectDto>> GetProjectsAsync(QueryParameters queryParameters);

        Task<ProjectDto> GetProjectByIdAsync(int id);

        Task AddProjectAsync(ProjectDto project);

        Task UpdateProjectAsync(ProjectDto project);

        Task DeleteProjectAsync(int id);
    }
}
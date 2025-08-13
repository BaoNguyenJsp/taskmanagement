using TaskManagement.Application;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Models;

namespace TaskManagement.Services
{
    public class ProjectService : IProjectService
    {
        public ProjectService(
            IBaseRepository<Project> projectRepository
            )
        {
            ProjectRepository = projectRepository;
        }

        public IBaseRepository<Project> ProjectRepository { get; }

        public async Task AddProjectAsync(ProjectDto project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var newProject = new Project
            {
                Name = project.Name,
                ProjectTasks = project.ProjectTasks.Select(pt => new ProjectTask
                {
                    Name = pt.Name
                }).ToList()
            };

            await ProjectRepository.AddAsync(newProject);
            await ProjectRepository.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid project ID", nameof(id));
            }

            await ProjectRepository.DeleteAsync(id);
            await ProjectRepository.SaveChangesAsync();
        }

        public async Task<ProjectDto> GetProjectByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid project ID", nameof(id));
            }

            var project = await ProjectRepository.GetByIdAsync(id, [
                    p => p.ProjectTasks
                ]);

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                ProjectTasks = project.ProjectTasks.Select(pt => new ProjectTaskDto
                {
                    Id = pt.Id,
                    Name = pt.Name
                }).ToList()
            };
        }

        public async Task<PagedResult<ProjectDto>> GetProjectsAsync(QueryParameters queryParameters)
        {
            var projects = await ProjectRepository.GetAllAsync(queryParameters, p => p.ProjectTasks);

            return new PagedResult<ProjectDto>
            {
                PageNumber = projects.PageNumber,
                PageSize = projects.PageSize,
                TotalCount = projects.TotalCount,
                Items = projects.Items.Select(project => new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    ProjectTasks = project.ProjectTasks.Select(pt => new ProjectTaskDto
                    {
                        Id = pt.Id,
                        Name = pt.Name
                    }).ToList()
                }).ToList()
            };
        }

        public async Task UpdateProjectAsync(ProjectDto project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (project.Id <= 0)
            {
                throw new ArgumentException("Invalid project ID", nameof(project.Id));
            }

            var existingProject = await ProjectRepository.GetByIdAsync(project.Id, [
                    p => p.ProjectTasks
                ]);

            existingProject.Name = project.Name;

            await ProjectRepository.SaveChangesAsync();
        }
    }
}

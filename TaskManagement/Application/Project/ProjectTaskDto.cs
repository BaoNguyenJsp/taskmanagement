
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application
{
    public class ProjectTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Pending;
    }
}

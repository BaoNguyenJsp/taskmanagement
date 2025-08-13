using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Pending;
    }
}

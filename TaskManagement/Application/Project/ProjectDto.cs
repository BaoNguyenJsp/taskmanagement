namespace TaskManagement.Application
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual IEnumerable<ProjectTaskDto> ProjectTasks { get; set; } = [];
    }
}

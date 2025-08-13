namespace TaskManagement.Domain.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int UserId { get; set; }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = [];
    }
}

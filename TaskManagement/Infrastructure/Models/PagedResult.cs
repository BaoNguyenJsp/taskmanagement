namespace TaskManagement.Infrastructure.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>(); // Data rows
        public int TotalCount { get; set; }                  // Total number of rows in DB
        public int PageNumber { get; set; }                  // Current page number
        public int PageSize { get; set; }                    // Number of records per page
    }

}

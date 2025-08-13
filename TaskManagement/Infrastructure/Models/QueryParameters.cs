namespace TaskManagement.Infrastructure.Models
{
    public class QueryParameters
    {
        // Paging
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Search (Field -> Value)
        public Dictionary<string, string> SearchFilters { get; set; } = new();
            
        // Sorting (Field -> "asc"/"desc")
        public Dictionary<string, bool> SortOrders { get; set; } = new();
    }

}

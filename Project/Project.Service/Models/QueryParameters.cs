using System.Data.SqlClient;

namespace Project.Service.Models
{
    public class QueryParameters 
    {
        public string Filter { get; } = string.Empty;
        public int Page { get; } = 1;
        public int PageSize { get; } = 10;
        public SortOrder Sort { get; } = SortOrder.Ascending;
    }
}

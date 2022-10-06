
namespace Project.Service.Models
{
    public class QueryParameters
    {
        public QueryParameters(string filter,
            int page,
            int pageSize,
            SortOrder sortOrder,
            SortBy sortBy
            )
        {
            Filter = filter;
            Page = page;
            PageSize = pageSize;
            SortOrder = sortOrder;
            SortBy = sortBy;
        }
        public string Filter { get; } = string.Empty;
        public int Page { get; } = 1;
        public int PageSize { get; } = 10;
        public SortOrder SortOrder { get; } = SortOrder.Ascending;
        public SortBy SortBy { get; } = SortBy.Name;
    }
}

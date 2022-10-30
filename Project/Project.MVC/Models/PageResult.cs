using Project.Service.Models;

namespace Project.MVC.Models
{
	public class PageResult<T> where T : class 
	{
		public PageResult(string filter,
        int page,
        int pageSize,
        SortOrder sortOrder,
		SortBy sortBy)
		{
			Query = new QueryParameters(filter, page, pageSize, sortOrder, sortBy);
		}
		public List<T> Data { get; set; } 
		public QueryParameters Query { get; set; }
	}
}
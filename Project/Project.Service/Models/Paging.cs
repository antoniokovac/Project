using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class Paging
    {
        public Paging(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; } = 1;
        public int PageSize { get; } = 10;
    }
}

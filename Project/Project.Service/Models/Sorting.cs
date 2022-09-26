using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class Sorting
    {
        public Sorting( SortOrder sort)
        {
            Sort = sort;
        }

        public SortOrder Sort { get; } = SortOrder.Ascending;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class Filtering
    {
        public Filtering(string filter)
        {
            Filter = filter;
        }

        public string Filter { get; } = string.Empty;
    }
}


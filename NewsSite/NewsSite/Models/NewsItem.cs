using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? MinAge { get; set; }
        public bool IsShown { get; set; }
    }
}

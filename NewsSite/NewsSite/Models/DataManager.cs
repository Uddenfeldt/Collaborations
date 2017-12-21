using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models
{
    public class DataManager
    {
        public static List<NewsItem> newsItems = new List<NewsItem>()
        {
            new NewsItem{ Id = 1000, CategoryName = "economy" },
            new NewsItem{ Id = 1001, CategoryName = "sport" },
            new NewsItem{ Id = 1002, CategoryName = "sport" },
            new NewsItem{ Id = 1003, CategoryName = "culture" },
            new NewsItem{ Id = 1004, CategoryName = "culture" },
            new NewsItem{ Id = 1005, CategoryName = "culture", MinAge = 20 },
        };

        public static List<NewsItem> getNewsItemsList()
        {
            return newsItems;
        }
    }
}

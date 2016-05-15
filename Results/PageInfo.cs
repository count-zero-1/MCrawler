using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler
{
    public class PageInfo
    {
        public Uri Url { get; set; }
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public List<string> Keywords { get; set; }
        public List<Uri> LinkedUrls { get; set; }

        public PageInfo()
        {
            LinkedUrls = new List<Uri>();
            Keywords = new List<string>();
        }
    }
}

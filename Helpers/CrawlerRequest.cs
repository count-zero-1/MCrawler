using MCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Helpers
{
    class CrawlerRequest : ICrawlerRequest
    {
        public Uri PageUrl { get; set; }

        public string GetResponse()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(PageUrl);
            try
            {
                var page = request.GetResponse();
                return new StreamReader(page.GetResponseStream()).ReadToEnd();
            }
            catch (WebException)
            {
                return string.Empty;
            }
        }
    }
}

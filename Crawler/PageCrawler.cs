using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MCrawler.Interfaces;
using System.IO;

namespace MCrawler.Crawler
{
    public class PageParsedEventArgs : EventArgs
    {
        public PageInfo Page { get; set; }
    }

    public class PageCrawler
    {
        IPageParser parser;
        ILogger logger;
        IDateTimeHelper dateTimeHelper;
        ICrawlerFactory factory;
        List<Uri> urls;
        int depthLeft;
        List<PageInfo> results;
        public delegate void PageParsedHandler(object sender, PageParsedEventArgs e);
        public event PageParsedHandler PageParsed;

        public PageCrawler(List<Uri> urls, int depthLeft, PageParsedHandler handler, IPageParser parser, ILogger logger, IDateTimeHelper dateTimeHelper, ICrawlerFactory factory)
        {
            this.parser = parser;
            this.logger = logger;
            this.dateTimeHelper = dateTimeHelper;
            this.urls = urls;
            this.depthLeft = depthLeft;
            this.PageParsed = handler;
            this.factory = factory;
            results = new List<PageInfo>();
        }

        public void Start()
        {
            if (depthLeft > 0)
            {
                foreach (var url in urls)
                {
                    var result = AnalyzePage(url);
                    PageParsed(this, new PageParsedEventArgs() { Page = result });
                    var crawler = factory.Create(result.LinkedUrls, depthLeft - 1, PageParsed);
                    crawler.Start();
                }

            }
        }
        public PageInfo AnalyzePage(Uri pageUri)
        {
            ICrawlerRequest webRequest = factory.CreateRequest(pageUri);
            var requestresultContent = webRequest.GetResponse();
            var result = parser.Parse(requestresultContent, pageUri, dateTimeHelper);
            return result;
        }
    }
}

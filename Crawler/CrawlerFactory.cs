using MCrawler.Helpers;
using MCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Crawler
{
    class CrawlerFactory : ICrawlerFactory
    {
        IPageParser pageParser;
        ILogger logger;
        IDateTimeHelper dateTimeHelper;

        public CrawlerFactory(IPageParser pageParser, ILogger logger, IDateTimeHelper dateTimeHelper)
        {
            this.pageParser = pageParser;
            this.logger = logger;
            this.dateTimeHelper = dateTimeHelper;
        }
        public PageCrawler Create(List<Uri> targetUris, int depthLeft, PageCrawler.PageParsedHandler handler)
        {
            return new PageCrawler(targetUris, depthLeft, handler, pageParser, logger, dateTimeHelper, this);
        }


        public ICrawlerRequest CreateRequest(Uri pageUri)
        {
            return new CrawlerRequest() { PageUrl = pageUri };
        }
    }
}

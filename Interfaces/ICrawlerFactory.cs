using MCrawler.Crawler;
using System;

namespace MCrawler.Interfaces
{
    public interface ICrawlerFactory
    {
        PageCrawler Create(System.Collections.Generic.List<Uri> targetUris, int depthLeft, PageCrawler.PageParsedHandler handler);

        ICrawlerRequest CreateRequest(Uri pageUri);
    }
}

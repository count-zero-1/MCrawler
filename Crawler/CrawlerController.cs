using MCrawler.Interfaces;
using Ninject;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Crawler
{
    class CrawlerController : ICrawlerController
    {
        ICrawlerSettings settings;
        ILogger logger;
        IResultWriter resultWriter;
        ICrawlerFactory crawlerFactory;
        ConcurrentDictionary<string, bool> urlsVisited;

        public CrawlerController(ICrawlerSettings settings, ILogger logger, IResultWriter resultWriter, ICrawlerFactory crawlerFactory)
        {
            this.settings = settings;
            this.logger = logger;
            urlsVisited = new ConcurrentDictionary<string, bool>();
            this.resultWriter = resultWriter;
            this.crawlerFactory = crawlerFactory;
        }

        
        public void Start()
        {
            settings.WriteToLogger(logger);
            var crawler = crawlerFactory.Create(new List<Uri>(){settings.StartUrl}, settings.Depth, PageParsed);
            crawler.Start();
        }

        void PageParsed(object sender, PageParsedEventArgs e)
        {
            resultWriter.WriteResult(e.Page);
        }
    }
}

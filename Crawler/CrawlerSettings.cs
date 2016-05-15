using MCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Crawler
{
    class CrawlerSettings : ICrawlerSettings
    {
        public int Depth { get; set; }
        public Uri StartUrl { get; set; }
        public string ResultFilename { get; set; }


        public CrawlerSettings()
        {
            Depth = 2;
            StartUrl = new Uri("http://apod.nasa.gov/apod/astropix.html", UriKind.Absolute);
            ResultFilename = "Results.txt";
        }

        public void WriteToLogger(ILogger logger)
        {
            logger.Info(string.Format("Depth: {0}", this.Depth));
            logger.Info(string.Format("StartUrl: {0}", this.StartUrl));
            logger.Info(string.Format("ResultFilename: {0}", this.ResultFilename));
        }
    }
}

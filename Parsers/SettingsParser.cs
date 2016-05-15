using MCrawler.Crawler;
using MCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Parsers
{
    class SettingsParser : ISettingsParser
    {
        ILogger logger;
        public SettingsParser(ILogger logger)
        {
            this.logger = logger;
        }

        public ICrawlerSettings ParseArguments(string[] args)
        {
            var settings = new CrawlerSettings();
            try
            {
                foreach (var item in args)
                {
                    var entry = item.Split('=');
                    switch (entry[0])
                    {
                        case "startUrl":
                            settings.StartUrl = new Uri(entry[1], UriKind.Absolute);
                            break;
                        case "depth":
                            int depth = 1;
                            if (int.TryParse(entry[1], out depth))
                            {
                                settings.Depth = depth;
                            }
                            break;
                        case "filename":
                            settings.ResultFilename = entry[1];
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Fatal("One of more of the arguments are invalid - using defaults", ex);
            }
            return settings;
        }

        public void WriteArgumentsInfo()
        {
            Console.WriteLine("You can run the program with the following parameters: startingUrl=[absoluteUri] depth=[int] filename=[filename]");
        }
    }
}

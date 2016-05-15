using log4net;
using MCrawler.Crawler;
using MCrawler.Helpers;
using MCrawler.Interfaces;
using MCrawler.Parsers;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            
            log4net.Config.XmlConfigurator.Configure();
            kernel.Bind<ISettingsParser>().To<SettingsParser>().InSingletonScope();
            kernel.Bind<IDateTimeHelper>().To<DateTimeHelper>().InSingletonScope();
            kernel.Bind<ILogger>().To<Logger>().InSingletonScope();
            kernel.Bind<ICrawlerController>().To<CrawlerController>().InSingletonScope();
            kernel.Bind<IPageParser>().To<PageParser>().InSingletonScope();
            kernel.Bind<ILog>().ToMethod(x => { return LogManager.GetLogger("ConsoleAppender"); });
            kernel.Bind<ICrawlerSettings>().ToMethod(x => kernel.Get<ISettingsParser>().ParseArguments(args));
            kernel.Bind<ICrawlerFactory>().To<CrawlerFactory>().InSingletonScope();
            
            kernel.Bind<IResultWriter>().ToMethod(x =>
            {
                return new ResultWriter(File.Create(kernel.Get<ICrawlerSettings>().ResultFilename));
            });

            kernel.Get<ISettingsParser>().WriteArgumentsInfo();

            var crawler = kernel.Get<ICrawlerController>();
            crawler.Start();
        }
    }
}

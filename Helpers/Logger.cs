using log4net;
using MCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Helpers
{
    class Logger : ILogger
    {
        ILog logger;

        public Logger(ILog logger)
        {
            this.logger = logger;
        }

        public void Error(object message, Exception exception = null)
        {
            if (exception == null)
                logger.Error(message);
            else
                logger.Error(message, exception);
        }


        public void Info(object message, Exception exception)
        {
            if (exception == null)
                logger.Info(message);
            else
                logger.Info(message, exception);
        }

        public void Debug(object message, Exception exception)
        {
            if (exception == null)
                logger.Debug(message);
            else
                logger.Debug(message, exception);
        }

        public void Fatal(object message, Exception exception)
        {
            if (exception == null)
                logger.Fatal(message);
            else
                logger.Fatal(message, exception);
        }
    }
}

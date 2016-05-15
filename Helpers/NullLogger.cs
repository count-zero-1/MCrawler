using MCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Helpers
{
    class NullLogger : ILogger
    {
        public void Error(object message, Exception exception = null)
        {
        }

        public void Info(object message, Exception exception = null)
        {
        }

        public void Debug(object message, Exception exception = null)
        {
        }

        public void Fatal(object message, Exception exception = null)
        {
        }
    }
}

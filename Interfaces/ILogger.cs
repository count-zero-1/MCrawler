using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Interfaces
{
    public interface ILogger
    {
        void Error(object message, Exception exception = null);
        void Info(object message, Exception exception = null);
        void Debug(object message, Exception exception = null);
        void Fatal(object message, Exception exception = null);
    }
}

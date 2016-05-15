using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Interfaces
{
    interface ISettingsParser
    {
        ICrawlerSettings ParseArguments(string[] args);
        void WriteArgumentsInfo();
    }
}

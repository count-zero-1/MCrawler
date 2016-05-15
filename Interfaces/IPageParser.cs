using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Interfaces
{
    public interface IPageParser
    {
        PageInfo Parse(string responseText, Uri responseUri, IDateTimeHelper dateTimeHelper);
    }
}

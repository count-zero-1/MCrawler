using MCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Helpers
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public DateTime GetTimestamp()
        {
            return DateTime.Now;
        }
    }
}

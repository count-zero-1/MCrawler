using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Interfaces
{
    public interface IResultWriter
    {
        void WriteResult(PageInfo info);
    }
}

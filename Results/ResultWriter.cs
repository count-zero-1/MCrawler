using MCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler
{
    class ResultWriter : IResultWriter, IDisposable
    {
        HashSet<string> ParsedPages;
        StreamWriter writer;
        
        public ResultWriter(Stream stream)
        {
            writer = new StreamWriter(stream);
            ParsedPages = new HashSet<string>();
        }
        
        public void WriteResult(PageInfo info)
        {
            
            if (!ParsedPages.Contains(info.Url.AbsoluteUri))
            {
                writer.WriteLine(string.Format("{0},{1},{2},{3}", info.Timestamp, info.Url, info.Title, string.Join("|", info.Keywords)));
                writer.Flush();
                ParsedPages.Add(info.Url.AbsoluteUri);
            }
        }

        public void Dispose()
        {
            if(writer != null)
            {
                writer.Close();
                writer.Dispose();
            }
        }
    }
}

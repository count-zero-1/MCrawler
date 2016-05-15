using System;
namespace MCrawler.Interfaces
{
    public interface ICrawlerSettings
    {
        int Depth { get; set; }
        string ResultFilename { get; set; }
        Uri StartUrl { get; set; }

        void WriteToLogger(ILogger logger);
    }
}

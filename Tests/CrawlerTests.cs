using MCrawler.Crawler;
using MCrawler.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Tests
{
    [TestFixture]
    class CrawlerTests
    {
        PageCrawler crawler;
        Mock<IPageParser> parserMock;
        Mock<ILogger> loggerMock;
        Mock<ICrawlerFactory> crawlerFactoryMock;
        Mock<IDateTimeHelper> dateTimeHelperMock;

        void PageParsedStub(object sender, PageParsedEventArgs e)
        {
            
        }

        [SetUp]
        public void Setup()
        {

            parserMock = new Mock<IPageParser>();
            loggerMock = new Mock<ILogger>();
            crawlerFactoryMock = new Mock<ICrawlerFactory>();
            dateTimeHelperMock = new Mock<IDateTimeHelper>();
        }

        [Test]
        public void Crawler_WhenCalled_CallsChildCrawlers()
        {
            var testUri = new Uri("http://example.org");
            var uris = new List<Uri>() { testUri };
            Mock<ICrawlerRequest> requestMock = new Mock<ICrawlerRequest>();
            crawlerFactoryMock.Setup(x => x.CreateRequest(testUri)).Returns(requestMock.Object);
            
            crawler = new PageCrawler(uris, 3, PageParsedStub, parserMock.Object, loggerMock.Object, dateTimeHelperMock.Object, crawlerFactoryMock.Object);
            
            crawler.Start();
        }
    }
}

using MCrawler.Interfaces;
using MCrawler.Parsers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Tests
{
    [TestFixture]
    class PageParserTests
    {
        const string TestPageCorrect =
@"<!doctype html>
<html>
<head>
    <title>Example Domain</title>

    <meta charset='utf-8' />
    <meta http-equiv='Content-type' content='text/html; charset=utf-8' />
    <meta name='keywords' content='kw1, kw2' />
    <meta name='description' content='test-description' />
</head>

<body>
<div>
    <h1>Example Domain</h1>
<a href='http://www.apod.org/domains/example111'>More information...</a>
    <p>This domain is established to be used for illustrative examples in documents. You may use this
    domain in examples without prior coordination or asking for permission.</p>
    <p><a href='http://www.iana.org/domains/example'>More information...</a></p>
</div>
</body>
</html>";
        const string TestPageIncorrect =
@"<!doctype html>
<html>
<head>
    <meta charset='utf-8' />
    <meta http-equiv='Content-type' content='text/html; charset=utf-8' />
    <meta name='keywords-malformed' content='kw1, kw2' />
</head>

<body>
<div>
    <h1>Example Domain</h1>
<a href='http://www.apod.org/domains/example111'>More information...</a>
    <p>This domain is established to be used for illustrative examples in documents. You may use this
    domain in examples without prior coordination or asking for permission.</p>
    <p><a href='http://www.iana.org/domains/example'>More information...</a></p>
</div>
</body>
</html>";

        PageParser parser;
        Mock<IDateTimeHelper> dateTimeHelperMock;
        DateTime timestamp = new DateTime(2015, 1, 11);
        [SetUp]
        public void Setup()
        {
            parser = new PageParser();
            dateTimeHelperMock = new Mock<IDateTimeHelper>();
            dateTimeHelperMock.Setup(x => x.GetTimestamp()).Returns(timestamp);
        }

        [Test]
        public void Parse_CorrectPage_GetsCorrectData()
        {
            var result = parser.Parse(TestPageCorrect, new Uri("http://example.org/"), dateTimeHelperMock.Object);

            Assert.That(result.Timestamp, Is.EqualTo(timestamp));
            Assert.That(result.Title, Is.EqualTo("Example Domain"));
            Assert.That(result.LinkedUrls, Is.EquivalentTo(new [] 
            { 
                new Uri("http://www.iana.org/domains/example"),
                new Uri("http://www.apod.org/domains/example111"),
            }));
            Assert.That(result.Keywords, Is.EquivalentTo(new []{"kw1", "kw2"}));
            Assert.That(result.Description, Is.EqualTo("test-description"));
        }

        [Test]
        public void Parse_IncorrectPage_GetsCorrectData()
        {
            var result = parser.Parse(TestPageIncorrect, new Uri("http://example.org/"), dateTimeHelperMock.Object);

            Assert.That(result.Timestamp, Is.EqualTo(timestamp));
            Assert.That(result.Title, Is.Empty);
            Assert.That(result.LinkedUrls, Is.EquivalentTo(new[] 
            { 
                new Uri("http://www.iana.org/domains/example"),
                new Uri("http://www.apod.org/domains/example111"),
            }));
            Assert.That(result.Keywords, Is.Empty);
            Assert.That(result.Description, Is.Empty);
        }
    }
}

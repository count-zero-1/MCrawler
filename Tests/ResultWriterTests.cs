using MCrawler.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Tests
{
    [TestFixture]
    public class ResultWriterTests
    {
        ResultWriter writer;
        MemoryStream stream;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();
            writer = new ResultWriter(stream);
        }

        [Test]
        public void WriteResult_DuplicateResults_WritesOnlyOne()
        {
            var result1 = new PageInfo() { Url = new Uri("http://example.org", UriKind.Absolute), Title="first-result" };
            var result2 = new PageInfo() { Url = new Uri("http://example.org", UriKind.Absolute), Title = "second-result" };

            writer.WriteResult(result1);
            writer.WriteResult(result2);
            
            stream.Position = 0;
            var writtenContent = new StreamReader(stream).ReadToEnd();
            Assert.That(writtenContent.Contains("first-result"), Is.True);
            Assert.That(writtenContent.Contains("second-result"), Is.False);
        }

        [Test]
        public void WriteResult_WhenCalled_WritesCorrectFormat()
        {
            var result1 = new PageInfo() { Url = new Uri("http://example.org/test1", UriKind.Absolute), Title = "first-result", Description = "test-description1", Keywords = new List<string>() { "kw1", "kw2" }, Timestamp = new DateTime(2016, 1, 11, 3, 4, 5) };
            var result2 = new PageInfo() { Url = new Uri("http://example.org/test2", UriKind.Absolute), Title = "second-result", Description = "test-description2", Keywords = new List<string>(), Timestamp = new DateTime(2016, 1, 11, 3, 4, 6) };

            writer.WriteResult(result1);
            writer.WriteResult(result2);

            stream.Position = 0;
            var writtenContent = new StreamReader(stream).ReadToEnd();
            var expectedResult = "2016-01-11 03:04:05,http://example.org/test1,first-result,kw1|kw2\r\n2016-01-11 03:04:06,http://example.org/test2,second-result,\r\n";
            Assert.That(writtenContent, Is.EqualTo(expectedResult));
        }
    }
}

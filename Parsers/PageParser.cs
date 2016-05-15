using HtmlAgilityPack;
using MCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MCrawler.Parsers
{
    class PageParser : IPageParser
    {
        public PageInfo Parse(string responseText, Uri responseUri, IDateTimeHelper dateTimeHelper)
        {
            HtmlDocument doc = new HtmlDocument();
            PageInfo info = new PageInfo();
            doc.LoadHtml(responseText);
            var documentNode = doc.DocumentNode;
            var head = documentNode.SelectSingleNode("//head");
            info.Title = GetTitle(head);
            info.Keywords = GetKeywords(head);
            info.Timestamp = dateTimeHelper.GetTimestamp();
            info.Url = responseUri;
            info.Description = GetDescription(head);
            info.LinkedUrls = GetLinkedUrls(documentNode, responseUri);


            return info;
        }

        private List<Uri> GetLinkedUrls(HtmlNode node, Uri responseUri)
        {
            List<Uri> result = new List<Uri>();
            if (node != null)
            {
                var urls = node.SelectNodes("//a");
                if (urls != null)
                {
                    foreach (var anchor in urls)
                    {
                        Uri link = null;
                        if (Uri.TryCreate(anchor.GetAttributeValue("href", "").ToString(), UriKind.RelativeOrAbsolute, out link))
                        {
                            if (!link.IsAbsoluteUri)
                            {
                                link = new Uri(responseUri, link.OriginalString);
                            }
                            result.Add(link);
                        }
                    }
                }
            }
            return result;
        }

        private string GetDescription(HtmlNode head)
        {
            if (head != null)
            {
                var description = head.SelectSingleNode("//meta[@name='description']");
                if (description != null)
                {
                    return description.GetAttributeValue("content", "");
                }
            }
            return string.Empty;
        }

        private List<string> GetKeywords(HtmlNode head)
        {
            if (head != null)
            {
                var keywords = head.SelectSingleNode("//meta[@name='keywords']");
                if (keywords != null)
                {
                    return keywords.GetAttributeValue("content", "").Split(',').Select(x => x.Trim()).ToList();
                }
            }
            return new List<string>();
        }

        private string GetTitle(HtmlNode head)
        {
            if (head != null)
            {
                var title = head.SelectSingleNode("//title");
                if (title != null)
                {
                    return title.InnerText;
                }
            }
            return string.Empty;
        }
    }
}

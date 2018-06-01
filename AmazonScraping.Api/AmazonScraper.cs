using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;

namespace AmazonScraping.Api
{
    public class AmazonScraper
    {
        public IEnumerable<AmazonItem> Scrape(string searchTerm)
        {
            var client = new WebClient();
            client.Headers["User-Agent"] = "foobar";
            string html = client.DownloadString($"https://www.amazon.com/s/?field-keywords={searchTerm}");
            var parser = new HtmlParser();
            var document = parser.Parse(html);
            return document.QuerySelectorAll("li.s-result-item").Select(ParseLi).Where(i => i != null);
        }

        private AmazonItem ParseLi(IElement li)
        {
            var h2 = li.QuerySelector("h2");
            if (h2 == null)
            {
                return null;
            }
            var result = new AmazonItem();
            result.Title = h2.TextContent;

            var aTag = li.QuerySelector("a.a-link-normal");
            if (aTag != null)
            {
                result.Url = aTag.Attributes["href"].Value;
            }

            var img = li.QuerySelector("img");
            result.ImageUrl =  img.Attributes["src"].Value;
            var priceSpan = li.QuerySelector("span.a-offscreen");
            if (priceSpan != null && priceSpan.TextContent != "[Sponsored]") 
            {
                result.Price = decimal.Parse(priceSpan.TextContent.Replace("$", ""));
            }

            return result;
        }
    }
}

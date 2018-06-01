using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using OpenQA.Selenium.Chrome;

namespace ConsoleApp81
{
    class Program
    {
        static void Main(string[] args)
        {
            //var client = new WebClient();
            //string html = client.DownloadString("http://lakewoodprogramming.com");

            //var parser = new HtmlParser();
            //IHtmlDocument document = parser.Parse(html);
            //IElement div = document.QuerySelector(".align-center");
            //IHtmlCollection<IElement> h2s = div.QuerySelectorAll("h2");
            //var h2Phone = h2s[1];
            //Console.WriteLine(h2Phone.TextContent);

            //var client = new WebClient();
            //client.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            //string firstName = Prompt("First Name?");
            //string lastName = Prompt("Last Name?");
            //string age = Prompt("Age?");

            //string data = $"firstName={firstName}&lastname={lastName}&age={age}";
            //client.UploadString("http://localhost:49820/assignmentpeople/add", data);
            //Console.WriteLine("Done");

            //var driver = new ChromeDriver();
            //driver.Navigate().GoToUrl("http://www.google.com");
            //driver.FindElementById("lst-ib").SendKeys("Lakewood Institute of Technology");
            //driver.FindElementByName("btnK").Submit();
            //driver.GetScreenshot().SaveAsFile("file.jpg");

            Console.WriteLine("Enter a search term");
            string search = Console.ReadLine();
            var client = new WebClient();
            client.Headers["User-Agent"] = "foobar";
            string html = client.DownloadString($"https://www.amazon.com/s/?field-keywords={search}");
            var parser = new HtmlParser();
            var document = parser.Parse(html);
            var lis = document.QuerySelectorAll("li.s-result-item");
            foreach (var li in lis)
            {
                ParseLi(li);
            }

            Console.ReadKey(true);
        }

        static string Prompt(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        static void ParseLi(IElement li)
        {
            var h2 = li.QuerySelector("h2");
            if (h2 == null)
            {
                return;
            }
            Console.WriteLine(h2.TextContent);

            var aTag = li.QuerySelector("a.a-link-normal");
            if (aTag != null)
            {
                Console.WriteLine(aTag.Attributes["href"].Value);
            }

            var img = li.QuerySelector("img");
            Console.WriteLine(img.Attributes["src"].Value);

            var priceSpan = li.QuerySelector("span.a-offscreen");
            if (priceSpan != null)
            {
                Console.WriteLine(priceSpan.TextContent);
            }
        }

    }
}

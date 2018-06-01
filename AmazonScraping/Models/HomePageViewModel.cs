using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AmazonScraping.Api;

namespace AmazonScraping.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<AmazonItem> Items { get; set; }
        public string SearchTerm { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmazonScraping.Api;
using AmazonScraping.Models;

namespace AmazonScraping.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string searchTerm)
        {
            var vm = new HomePageViewModel();
            if (String.IsNullOrEmpty(searchTerm))
            {
                return View(vm);
            }

            vm.SearchTerm = searchTerm;
            var scraper = new AmazonScraper();

            vm.Items = scraper.Scrape(searchTerm);
            return View(vm);
        }
    }
}
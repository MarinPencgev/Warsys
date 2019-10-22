using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Warsys.Data;
using Warsys.Services;
using Warsys.Web.Models;

namespace Warsys.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISeederService _seeder;

        public HomeController(ILogger<HomeController> logger, ISeederService seeder)
        {
            _logger = logger;
            _seeder = seeder;
        }

        public IActionResult Index()
        {
            //bool seeding = _seeder.SeedFromExcel(@"C:\Users\Nora\Desktop\Warsys\SeedFile-Products.xlsx");
            bool seeding = _seeder.SeedFromExcel(@"C:\Users\Nora\Desktop\Warsys\SeedFile-Transactions.xlsx");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

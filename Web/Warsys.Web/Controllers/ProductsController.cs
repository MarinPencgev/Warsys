using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Warsys.Services;
using Warsys.Web.Models.ViewModels;

namespace Warsys.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ITransactionsService _transactionsService;

        public ProductsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [ActionName("Gasoline")]
        public IActionResult Index()
        {
            var all = _transactionsService.GetAll()
                .Where(x => x.Product.ExciseCode == "E420")
                .Select(x => new TransactionsViewModel
                {
                    Flowmeter = x.DeviceId,
                    TransactionNumber = x.Sequence,
                    EndDate = x.StopTime.ToString(),
                    Receipt = x.Product.Name,
                    DensityAt15 = x.Density15.ToString(),
                    Mass = x.Mass.ToString(),
                    VolumeAt15 = x.StdVolume.ToString(),
                    FlowDirection = x.Direction.Direction.ToString()
                });

            var byDate = all.Where(x => x.FlowDirection == "OUTPUT")
                .Select(x => new
                {
                    Date = x.EndDate.Substring(0, 10),
                    Volume15 = decimal.Parse(x.VolumeAt15),
                    Mass = decimal.Parse(x.Mass),
                })
                .GroupBy(x => x.Date)
                .Select(g => new TransactionsByDateViewModel
                {
                    Date = g.Key,
                    Volume15 = g.Sum(x => x.Volume15).ToString(),
                    Mass = g.Sum(x => x.Mass).ToString()
                });

            var modelsContainer = new List<object>();
            modelsContainer.Add(all);
            modelsContainer.Add(byDate);

            return this.View(modelsContainer);
        }
    }
}

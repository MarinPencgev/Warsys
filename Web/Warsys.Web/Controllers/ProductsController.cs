using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var model = _transactionsService.GetAll()
                .Where(x => x.Product.ExciseCode == "E420")
                .Select(x => new TransactionsViewModel
                {
                    Flowmeter = x.DeviceId,
                    TransactionNumber = x.Sequence,
                    EndDate = x.StopTime.ToString(),
                    Receipt = x.Product.Name,
                    DensityAt15 = x.Density15.ToString(),
                    Mass = x.Mass.ToString(),
                    VolumeAt15 = x.StdVolume.ToString()
                });


            return this.View(model);
        }
    }
}

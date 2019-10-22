using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warsys.Services;

namespace Warsys.Web.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }
        

    }
}

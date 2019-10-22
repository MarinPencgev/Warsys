using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Warsys.Data;
using Warsys.Data.Models;

namespace Warsys.Services
{
    public class TransactionsService: ITransactionsService
    {
        private readonly WarsysDbContext _context;

        public TransactionsService(WarsysDbContext context)
        {
            _context = context;
        }

        public ICollection<Transaction> GetAll()
        {
            var transactions = _context.Transactions
                .Include(x => x.Product)
                .Include(x=>x.Direction)
                .OrderByDescending(x => x.StopTime)
                .ToList();
                
            return transactions;
        }
    }
}

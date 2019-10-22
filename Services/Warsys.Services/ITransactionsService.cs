using System;
using System.Collections.Generic;
using System.Text;
using Warsys.Data.Models;

namespace Warsys.Services
{
    public interface ITransactionsService
    {
        ICollection<Transaction> GetAll();

    }
}

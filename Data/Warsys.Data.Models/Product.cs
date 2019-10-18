using System;
using System.Runtime.InteropServices;

namespace Warsys.Data.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string ProductCode { get; set; }

    }
}

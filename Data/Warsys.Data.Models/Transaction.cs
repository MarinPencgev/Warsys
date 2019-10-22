using System;
using System.Collections.Generic;
using System.Text;

namespace Warsys.Data.Models
{
    public class Transaction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Sequence { get; set; }

        public DateTime StopTime { get; set; }

        public string DeviceId { get; set; }

        public FlowDirection Direction { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string DocumentRefference { get; set; }

        public decimal Volume { get; set; }

        public decimal StdVolume { get; set; }

        public decimal Mass { get; set; }

        public decimal DensityT { get; set; }

        public decimal Density15 { get; set; }

        public decimal Temperature { get; set; }

    }
}

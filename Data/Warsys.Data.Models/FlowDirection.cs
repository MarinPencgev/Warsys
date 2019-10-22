using System;
using System.Collections.Generic;
using System.Text;

namespace Warsys.Data.Models
{
    public class FlowDirection
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Direction { get; set; } 

    }
}

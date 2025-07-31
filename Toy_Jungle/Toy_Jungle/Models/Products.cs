using System;
using System.Collections.Generic;

namespace ThePlayCastle.Models
{
    public partial class Products
    {
        public long Pid { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThePlayCastle.Models
{
    public partial class OrderInfo
    {
        [Key]
        public long Odid { get; set; }
        public long? Pid { get; set; }
        public decimal? OrderNumber { get; set; }
        public long? Quantity { get; set; }
        public long? TotalPrice { get; set; }
        public long? Price { get; set; }
        public string ProductName { get; set; }
    }
}

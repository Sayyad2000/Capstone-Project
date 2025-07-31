using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThePlayCastleMVC.Models
{
    public partial class OrderInfo
    {
        [Key]
        public long Odid { get; set; }
        public long? Pid { get; set; }
        public string ProductName { get; set; }
        public long? OrderNumber { get; set; }
        public long? Quantity { get; set; }
        public long Price { get; set; }
        public long TotalPrice { get; set; }
    }
}

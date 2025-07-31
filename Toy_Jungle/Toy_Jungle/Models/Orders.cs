using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThePlayCastle.Models
{
    public partial class Orders
    {
        public decimal Oid { get; set; }
        public decimal OrderNumber { get; set; }
        public DateTime? OrderDateTime { get; set; } = DateTime.Now;
        public decimal? Uid { get; set; }
        public decimal? BillAmount { get; set; }


        [NotMapped]
        public List<OrderInfo>? Items { get; set; }
    }
}

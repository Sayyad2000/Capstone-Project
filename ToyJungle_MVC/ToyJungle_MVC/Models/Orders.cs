using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThePlayCastleMVC.Models
{
    public partial class Orders
    {
        public decimal Oid { get; set; }
        public decimal OrderNumber { get; set; }
        public DateTime? OrderDateTime { get; set; }
        public decimal? Uid { get; set; }
        public decimal? BillAmount { get; set; }

        [NotMapped]
        public List<OrderInfo>? Items { get; set; }
    }
}

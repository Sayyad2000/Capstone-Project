using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThePlayCastleMVC.Models
{
    public partial class Users
    {
        public long Uid { get; set; }
        public string UserName { get; set; } = "User";
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; }
        public string Address { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace ThePlayCastle.Models
{
    public partial class Users
    {
        public long Uid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; } = "User";
        public string Address { get; set; }
    }
}

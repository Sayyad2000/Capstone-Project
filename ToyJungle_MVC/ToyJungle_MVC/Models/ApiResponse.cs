using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThePlayCastleMVC.Models
{
    public class ApiResponse
    {
        public string Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

    }
}

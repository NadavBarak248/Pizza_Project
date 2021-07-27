using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class Branch
    {
        public int id { get; set; }

        public string Branch_name { get; set; }

        public string location { get; set; }

        public List<Order> Branch_orders { get; set; }
    }
}

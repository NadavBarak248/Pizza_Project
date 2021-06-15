using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class Order
    {
        public int Id { get; set; }

        public User User_order { get; set; }

        public List<Pizza> Pizza_order { get; set; }

        public DateTime Order_date { get; set; }

        public DateTime Expected_delivery { get; set; }

        public DateTime Time_delivered { get; set; }

        public double Price { get; set; }

        public string Location { get; set; }
    }
}

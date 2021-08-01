using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class PizzasInOrder
    {
        public Pizza PizzaId { get; set; }

        public Order OrderId { get; set; }

        public int Quantity { get; set; }
    }
}

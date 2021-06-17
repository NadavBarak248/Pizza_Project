using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class Slice
    {
        public int Id { get; set; }

        public int PizzaId { get; set; }
       
        [Required]
        public Pizza Pizza { get; set; }

        public List<Topping> Toppings{ get; set; }

        public double Orders_number { get; set; }
    }
}

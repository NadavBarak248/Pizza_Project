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
       
        [Display(Name = "Pizza Id")]
        public int PizzaId { get; set; }
       
        [Required(ErrorMessage = "you must select the Pizza")]
        public Pizza Pizza { get; set; }

        public List<Topping> Toppings{ get; set; }

        [Display(Name = "Order Number")]
        public double Orders_number { get; set; }
    }
}

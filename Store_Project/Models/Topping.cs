using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class Topping
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you must input the topping name")]
        [RegularExpression(@"^[a-zA-Z \-]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        [Range(0,10)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(Name="Pizza Toppings")]
        public List<Pizza> Toppings_pizza { get; set; }
    }
    
}

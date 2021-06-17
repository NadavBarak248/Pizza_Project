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
        [StringLength(10)]
        [Required(ErrorMessage = "you must input the topping name")]
        public string Name { get; set; }

        [Range(0,10)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public List<Slice> toppingsSlices { get; set; }
    }
    
}

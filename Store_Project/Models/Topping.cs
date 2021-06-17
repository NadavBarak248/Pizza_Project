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

        public string Name { get; set; }

        public double Price { get; set; }

        public List<Slice> toppingsSlices { get; set; }
    }
    
}

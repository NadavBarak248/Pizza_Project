using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class PizzaImage
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }
    }
}

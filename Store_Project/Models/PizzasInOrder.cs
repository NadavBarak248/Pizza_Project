using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class PizzasInOrder
    {
        //public int Id { get; set; }

        [Key]
        [Column(Order = 0)]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Orders { get; set; }


        [Key]
        [Column(Order=1)]
        public int PizzaId { get; set; }

        [ForeignKey("PizzaId")]
        public virtual Pizza Pizzas{ get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        [Required, Display(Name ="User order")]
        public User User_order { get; set; }

        [Display(Name = "Order Date")]
        public DateTime Order_date { get; set; }

        public double Price { get; set; }

        public int BranchId { get; set; }

        [Required]
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        [Display(Name = "Pizzas in Order")]
        public List<PizzasInOrder> PizzaOrder { get; set; }
    }
}

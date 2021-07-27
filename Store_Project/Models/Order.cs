using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required, Display(Name ="User order")]
        public User User_order { get; set; }

        [Display(Name = "pizza order")]
        public List<Pizza> Pizza_order { get; set; }

        [Display(Name = "Order Date")]
        public DateTime Order_date { get; set; }

        public double Price { get; set; }

        [Required]
        public string Location { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public enum User_type
    {
        Guest,
        Customer,
        Manager        
    }
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you must enter the user name")]
        public String Username { get; set; }

        [RegularExpression("^[A-Z]+[a-zA-Z0-9]*$", ErrorMessage = "password must be .... ")] 
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public User_type Type { get; set; } = User_type.Guest;

        public List<Order> User_orders { get; set; }
    }
}

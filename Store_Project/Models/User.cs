using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public enum User_type
    {
        Admin,
        Client
    }
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you must enter the user name")]
        [RegularExpression("[a-zA-Z0-9]*", ErrorMessage = "username can contain letters and digits only")]
        public String Username { get; set; }

        [RegularExpression("^[A-Z]+[a-zA-Z0-9]*$", ErrorMessage = "password must start with a capital letter ")] 
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public User_type Type { get; set; }

        public List<Order> User_orders { get; set; }
    }
}

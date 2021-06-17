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
        Delivery_person,
        Manager        
    }
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you must enter the user name"),]
        public String Username { get; set; }

        //[RegularExpression("^[A-Z]+[a-zA-Z0-9]*$", ErrorMessage = "password must be .... ")] אם מוסיפים צריך ליצור את הcontroller שוב ולעדכן DB
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public User_type Type { get; set; } = User_type.Customer;

        public List<Order> User_orders { get; set; }
    }
}

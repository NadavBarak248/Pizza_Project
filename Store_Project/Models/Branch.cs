using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class Branch
    {
        public int id { get; set; }

        [Required(ErrorMessage = "you must input the branch name"), Display(Name = "Branch Name")]
        [RegularExpression(@"^[a-zA-Z \-]+$", ErrorMessage = "Use letters only please")]
        public string Branch_name { get; set; }

        [Required(ErrorMessage = "you must input the branch location"), Display(Name = "Branch Location")]
        public string location { get; set; }

        [Display(Name ="Branch Orders")]
        public List<Order> Branch_orders { get; set; }
    }
}

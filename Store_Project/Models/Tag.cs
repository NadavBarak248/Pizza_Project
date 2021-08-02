using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you must input Tag name"), Display(Name = "Tag")]
        [RegularExpression(@"^[a-zA-Z \-]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        [DataType("Color"), Display(Name = "Tag Color")]
        [Required(ErrorMessage = "you must input Tag color")]
        public string Color { get; set; }

        [Display(Name="Pizza Tags")]
        public List<Pizza> Pizza_tag { get; set; }
    }
}

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

        [Display(Name = "תגית")]
        public string Name { get; set; }

        [Display(Name="פיצות")]
        public List<Pizza> Pizza_tag { get; set; }
    }
}

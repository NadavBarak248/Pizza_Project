using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    public class PizzaImage
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }

        [NotMapped]
        [Display(Name="Image")]
        public IFormFile ImageFile { get; set; }

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }
    }
}

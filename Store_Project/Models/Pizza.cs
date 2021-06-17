using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Project.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Size
    {
        אישית,
        בינונית,
        משפחתית,
        ענקית
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Width
    {
        דק,
        עבה
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Sauce
    {
        עגבניות,
        פסטו,
        שמנת
    }

    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you must input the Pizza name"), Display(Name = "Pizza Name")]
        public string Name { get; set; }
       
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        // 20 + 10*size + slice*toppings

        [Required(ErrorMessage = "you must input the Pizza size"), Display(Name = "Pizza size")]
        public Size Pizza_size { get; set; }

        [Display(Name = "toppings")]
        public List<Slice> Pizza_slices { get; set; }

        [Required(ErrorMessage = "you must input the Pizza width"), Display(Name = "Dough thickness")]
        public Width Pizza_width { get; set; }

        [Display(Name ="Pizza sauce")]
        public Sauce Pizza_sauce { get; set; }

        [Display(Name="with cheese")]
        public bool With_cheese { get; set; }

        [Display(Name="Tags")]
        public List<Tag> Pizza_tags { get; set; }

        [Display(Name="Pizza Image")]
        public PizzaImage Pizza_image { get; set; }

        [Display(Name ="Display")]
        public bool To_present { get; set; }

        [Display(Name ="Orders")]
        public List<Order> Order_pizza{ get; set; }
    }
}

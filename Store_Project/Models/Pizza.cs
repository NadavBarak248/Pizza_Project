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
        Small,
        Medium,
        Large,
        XL
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Crust
    {
        Thin,
        Thick,
        [Display(Name = "Gluten Free")]
        Gluten_Free
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Sauce
    {
        Tomato,
        Pesto,
        Alfredo
    }

    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you must input the Pizza name"), Display(Name = "Pizza Name")]
        [RegularExpression(@"^[a-zA-Z \-]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid double Number")]
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        // 20 + 10*size + slice*toppings

        [Required(ErrorMessage = "you must input the Pizza size"), Display(Name = "Pizza Size")]
        public Size Pizza_size { get; set; }

        [Display(Name = "Toppings")]
        public List<Topping> Pizza_toppings { get; set; }

        [Required(ErrorMessage = "you must select a Crust"), Display(Name = "Crust")]
        public Crust Pizza_width { get; set; }

        [Display(Name ="Pizza Sauce")]
        [Required(ErrorMessage = "you must select sauce")]
        public Sauce Pizza_sauce { get; set; }

        [Display(Name="with cheese")]
        public bool With_cheese { get; set; }

        [Display(Name="Tags")]
        public List<Tag> Pizza_tags { get; set; }

        [Display(Name="Pizza Image")]
        public PizzaImage Pizza_image { get; set; }

        [Display(Name ="Display")]
        public bool To_present { get; set; }

        [Display(Name ="Pizzas in Order")]
        public List<PizzasInOrder> PizzaOrder{ get; set; }

    }
}

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

        [Display(Name = "שם")]
        public string Name { get; set; }

        [Display(Name = "מחיר")]
        public double Price { get; set; }
        // 20 + 10*size + slice*toppings

        [Display(Name = "גודל")]
        public Size Pizza_size { get; set; }

        [Display(Name = "תוספות")]
        public List<Slice> Pizza_slices { get; set; }

        [Display(Name = "עובי")]
        public Width Pizza_width { get; set; }

        [Display(Name ="רוטב")]
        public Sauce Pizza_sauce { get; set; }

        [Display(Name="עם גבינה")]
        public bool With_cheese { get; set; }

        [Display(Name="תגיות")]
        public List<Tag> Pizza_tags { get; set; }

        [Display(Name="תמונה")]
        public PizzaImage Pizza_image { get; set; }

        [Display(Name ="להציג")]
        public bool To_present { get; set; }

        [Display(Name ="הזמנות")]
        public List<Order> Order_pizza{ get; set; }
    }
}

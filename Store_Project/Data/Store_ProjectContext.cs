using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store_Project.Models;

namespace Store_Project.Data
{
    public class Store_ProjectContext : DbContext
    {
        public Store_ProjectContext (DbContextOptions<Store_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Store_Project.Models.Topping> Topping { get; set; }

        //public DbSet<Store_Project.Models.ToppingImage> ToppingImage { get; set; }

        public DbSet<Store_Project.Models.Pizza> Pizza { get; set; }

        public DbSet<Store_Project.Models.Tag> Tag { get; set; }

        public DbSet<Store_Project.Models.User> User { get; set; }

        public DbSet<Store_Project.Models.Order> Order { get; set; }

        public DbSet<Store_Project.Models.PizzaImage> PizzaImage { get; set; }

        public DbSet<Store_Project.Models.Branch> Branch { get; set; }
    }
}

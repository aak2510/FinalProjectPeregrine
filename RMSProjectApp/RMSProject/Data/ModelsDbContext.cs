using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RMSProject.Models;

namespace RMSProject.Data
{
    public class ModelsDbContext : DbContext
    {
        public ModelsDbContext (DbContextOptions<ModelsDbContext> options)
            : base(options)
        {
        }

        public DbSet<MenuItem> MenuItem { get; set; } = default!;

        public DbSet<NutritionalInformation> NutritionalInformation { get; set; } = default!;

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;} = default!;

        public DbSet<OrderItemInBasket> OrderItemInBasket { get; set; } = default!;

        public DbSet<OrderInformation> OrderInformation { get; set; } = default!;

    }
}

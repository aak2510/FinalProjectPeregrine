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

        public DbSet<RMSProject.Models.MenuItem> MenuItem { get; set; } = default!;

        public DbSet<RMSProject.Models.Ingredients> Ingredients { get; set; } = default!;
        
        public DbSet<RMSProject.Models.MenuIngredients> MenuIngredients { get; set; } = default!;

        // Override used to explicitly tell EF core that both the IngredientsId and MenuItemId
        // are indeed the composite primary key for the junction table (MenuIngredients)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuIngredients>()
                .HasKey(mi => new { mi.IngredientsId, mi.MenuItemId });
        }

    }
}

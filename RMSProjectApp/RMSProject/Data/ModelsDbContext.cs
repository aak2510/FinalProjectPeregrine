﻿using System;
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
    }
}
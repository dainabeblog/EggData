using System;
using System.Collections.Generic;
using System.Text;
using EggData_dev.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EggData_dev.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Store> Store { get; set; }

        public DbSet<SalesData> SalesData { get; set; }

    }
}

using CircleDrawingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CircleDrawingApp
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Circle>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Circle> Circles { get; set; }
    }
}

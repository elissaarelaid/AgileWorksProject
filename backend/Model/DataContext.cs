using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Application>? Applications { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Application>().HasData(
                new Application {
                    Id = 1,
                    Description = "Application1",
                    EntryDate = DateTime.Now
                },
                new Application {
                    Id = 2,
                    Description = "Application2",
                    EntryDate = DateTime.Now
                },
                 new Application {
                    Id = 3,
                    Description = "Application2",
                    EntryDate = DateTime.Now
                }
            );
        }
    }
}
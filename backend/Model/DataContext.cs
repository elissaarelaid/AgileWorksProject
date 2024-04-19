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
                    EntryDate = DateTime.Now,
                    ResolutionDate = new DateTime(2024,12,12),
                    IsSolved = true
                },
                new Application {
                    Id = 2,
                    Description = "Application2",
                    EntryDate = DateTime.Now,
                    ResolutionDate = new DateTime(2029,12,12)
                },
                 new Application {
                    Id = 3,
                    Description = "Application3",
                    EntryDate = DateTime.Now,
                    ResolutionDate = new DateTime(2028,12,12)
                },
                new Application {
                    Id = 4,
                    Description = "Application4",
                    EntryDate = DateTime.Now,
                    ResolutionDate = new DateTime(2026,12,12)
                },
                 new Application {
                    Id = 5,
                    Description = "Application5",
                    EntryDate = DateTime.Now,
                    ResolutionDate = new DateTime(2030,12,12)
                }
            );
        }
    }
}
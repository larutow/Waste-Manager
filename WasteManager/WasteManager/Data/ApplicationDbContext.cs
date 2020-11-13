using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WasteManager.Models;

namespace WasteManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = "dd57f3cb-54b6-4aaf-ac41-b8ff9a22ca8d",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = "360217b0-d9be-4a4d-9317-4f0eb0b3c7fe"
                    }
                );

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = "2df5c37f-a80f-49e5-9753-408f3036c8fb",
                        Name = "Customer",
                        NormalizedName = "CUSTOMER",
                        ConcurrencyStamp = "1177775b-0409-47db-a323-2710e24c0f01"
                    }
                );

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = "0bbfff3b-1110-49b3-bf07-d783215f0ab8",
                        Name = "Employee",
                        NormalizedName = "EMPLOYEE",
                        ConcurrencyStamp = "6978ca47-329f-4470-a523-8eb2c88c4882"
                    }
                );

        }
    }
}

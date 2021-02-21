using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SingularityAssesment
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasData(
                    new Product { Id = 101, Name = "Product 101", Description = "Product 101", Price = 10 },
                    new Product { Id = 202, Name = "Product 103", Description = "Product 102", Price = 20 },
                    new Product { Id = 303, Name = "Product 102", Description = "Product 103", Price = 30 }
                );
        }
    }
}

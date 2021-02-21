using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SingularityAssesment;
using SingularityAssesment.Models;

namespace SingularityAssesment.Data
{
    public class SingularityAssesmentContext : DbContext
    {
        public SingularityAssesmentContext (DbContextOptions<SingularityAssesmentContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
        }
    }
}

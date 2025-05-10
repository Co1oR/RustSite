using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationDomain;

namespace ApplicationContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().HasKey(x => x.Id);

            modelBuilder.Entity<Bin>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Bin)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<Bin>()
                .HasOne(x => x.User)
                .WithMany(x => x.Bin)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Bin>().HasKey(x => x.Id);


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bin> Bins { get; set; }
    }
}

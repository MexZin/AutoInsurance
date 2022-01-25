using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.EF
{
    public partial class AutoDBContext : DbContext
    {
        public AutoDBContext()
            : base("name=AutoDBContext")
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Package>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Package)
                .WillCascadeOnDelete(false);
        }
    }
}

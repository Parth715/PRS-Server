using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_Server.Models
{
    public class PRSDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestLine> RequestLines { get; set; }

        public PRSDbContext(DbContextOptions<PRSDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(j =>
            {
                j.ToTable("Users");
                j.HasKey(p => p.Id);
                j.Property(p => p.Username).HasMaxLength(30).IsRequired(true);
                j.HasIndex(p => p.Username).IsUnique(true);
                j.Property(p => p.Password).HasMaxLength(30).IsRequired(true);
                j.Property(p => p.Firstname).HasMaxLength(30).IsRequired(true);
                j.Property(p => p.Lastname).HasMaxLength(30).IsRequired(true);
                j.Property(p => p.Phone).HasMaxLength(12);
                j.Property(p => p.Email).HasMaxLength(255);
                j.Property(p => p.IsReviewer);
                j.Property(p => p.IsAdmin);
            });

            builder.Entity<Vendor>(j =>
            {
                j.ToTable("Vendors");
                j.HasKey(p => p.Id);
                j.Property(p => p.Code).HasMaxLength(30).IsRequired(true);
                j.HasIndex(p => p.Code).IsUnique(true);
                j.Property(p => p.Name).HasMaxLength(30).IsRequired(true);
                j.Property(p => p.Address).HasMaxLength(30).IsRequired(true);
                j.Property(p => p.City).HasMaxLength(30).IsRequired(true);
                j.Property(p => p.State).HasMaxLength(2).IsRequired(true);
                j.Property(p => p.Zip).HasMaxLength(5).IsRequired(true);
                j.Property(p => p.Phone).HasMaxLength(12);
                j.Property(p => p.Email).HasMaxLength(255);
            });

            builder.Entity<Product>(j => {
                j.ToTable("Products");
                j.HasKey(p => p.Id);
                j.Property(p => p.PartNbr).HasMaxLength(30).IsRequired(true);
                j.HasIndex(p => p.PartNbr).IsUnique(true);
                j.Property(p => p.Name).HasMaxLength(30).IsRequired(true);
                j.Property(p => p.Price).HasColumnType("decimal(8,2)").IsRequired(true);
                j.Property(p => p.Unit).HasMaxLength(30).IsRequired(true);
                j.Property(p => p.Photopath).HasMaxLength(255);
                j.HasOne(p => p.Vendor).WithMany(p => p.Products).HasForeignKey(p => p.VendorId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Request>(j => {
                j.ToTable("Requests");
                j.HasKey(p => p.Id);
                j.Property(p => p.Description).HasMaxLength(80).IsRequired(true);
                j.Property(p => p.Justification).HasMaxLength(80).IsRequired(true);
                j.Property(p => p.RejectionReason).HasMaxLength(80);
                j.Property(p => p.DeliveryMode).HasMaxLength(20).IsRequired(true);
                j.Property(p => p.Status).HasMaxLength(10).IsRequired(true);
                j.Property(p => p.Total).HasColumnType("decimal(11,2)").IsRequired(true);
                j.HasOne(p => p.User).WithMany(p => p.Requests).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<RequestLine>(j =>
            {
                j.ToTable("RequestLines");
                j.HasKey(p => p.Id);
                j.HasOne(p => p.Request).WithMany(p => p.RequestLines).HasForeignKey(p => p.RequestId).OnDelete(DeleteBehavior.Restrict);
                j.HasOne(p => p.Product).WithMany(p => p.RequestLines).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Restrict);
                j.Property(p => p.Quantity).IsRequired(true);
            });
        }
        
        
    }
}

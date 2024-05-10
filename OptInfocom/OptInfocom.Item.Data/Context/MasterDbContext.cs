using Microsoft.EntityFrameworkCore;
using OptInfocom.Item.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Data.Context
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure table names and primary keys
            modelBuilder.Entity<Company>().ToTable("erp_company_master").HasKey(c => c.company_id);
            modelBuilder.Entity<Division>().ToTable("erp_division_master").HasKey(d => d.division_id);
            modelBuilder.Entity<User>().ToTable("erp_user_master").HasKey(u => u.ErpUserId);

            // Configure relationships
            modelBuilder.Entity<Division>()
                .HasOne(d => d.Company)
                .WithMany(c => c.Divisions)
                .HasForeignKey(d => d.company_id);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Division)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DivisionId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OptInfocom.Item.Domain.Models;

namespace OptInfocom.Item.Data.Context
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {

        }

        //public DbSet<DivisionMaster> DbDivisionMaster { get; set; }
        public DbSet<ItemMaster> DbItemMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<DivisionMaster>().ToTable("erp_division_master").HasKey(k => k.division_id);
            modelBuilder.Entity<ItemMaster>().ToTable("mst_item_master").HasKey(k => k.item_id);
        }
    }
}


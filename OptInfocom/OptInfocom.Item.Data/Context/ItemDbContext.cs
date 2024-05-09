using Microsoft.EntityFrameworkCore;
using OptInfocom.Item.Domain.Models;

namespace OptInfocom.Item.Data.Context
{
    public class ItemDbContext : DbContext//, IItemDbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {

        }

        public DbSet<ItemMaster> DbItemMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemMaster>().ToTable("mst_item_master").HasKey(k => k.item_id);
        }
    }
}


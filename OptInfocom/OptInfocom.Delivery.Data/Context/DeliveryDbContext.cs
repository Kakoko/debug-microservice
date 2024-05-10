using Microsoft.EntityFrameworkCore;
using OptInfocom.Delivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Delivery.Data.Context
{
    public class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions options) : base(options) 
        { 

        }
        public DbSet<DeliveryStatus> DbDeliveryStatus { get; set; }
        public DbSet<SalesInvoiceMaster> DbSalesInvoiceMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryStatus>().ToTable("log_track_status").HasKey(k => k.id);
            modelBuilder.Entity<SalesInvoiceMaster>().ToTable("mst_sales_invoice").HasKey(k => k.invoice_id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OptInfocom.Delivery.Data.Context;
using OptInfocom.Delivery.Domain.Interfaces;
using OptInfocom.Delivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Delivery.Data.Repository
{
    public class DeliveryStatusRepository : IDeliveryStatusRepository
    {
        private DeliveryDbContext _ctx;
        public DeliveryStatusRepository(DeliveryDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IEnumerable<DeliveryStatus>> GetByInvoiceIDAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _ctx.DbDeliveryStatus.Where(w => w.master_id == id).ToListAsync(cancellationToken);
            return result;
        }

        public async Task<bool> SaveAsync(DeliveryStatus entity, CancellationToken cancellationToken)
        {
            _ctx.DbDeliveryStatus.Add(entity);
            await _ctx.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

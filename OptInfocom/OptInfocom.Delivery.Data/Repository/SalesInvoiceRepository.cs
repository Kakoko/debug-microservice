using Microsoft.EntityFrameworkCore;
using OptInfocom.Delivery.Data.Context;
using OptInfocom.Delivery.Data.Extension;
using OptInfocom.Delivery.Domain.Interfaces;
using OptInfocom.Delivery.Domain.Models;

namespace OptInfocom.Delivery.Data.Repository
{
    public class SalesInvoiceRepository : ISalesInvoiceRepository
    {
        private DeliveryDbContext _ctx;
        public SalesInvoiceRepository(DeliveryDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<SalesInvoiceMaster> GetByInvoiceIDAsync(int id, CancellationToken cancellationToken = default)
        {
            int FinancialYear = DateTime.Now.ToString("dd-MM-yyyy").ExtChangeDateTimeFormat().ExtFinancialYear();
            var result = await _ctx.DbSalesInvoiceMaster.Where(w => w.invoice_id == id && w.financial_year == FinancialYear).FirstOrDefaultAsync(cancellationToken);
            return result;
        }

        public async Task<SalesInvoiceMaster> GetByInvoiceCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            int FinancialYear = DateTime.Now.ToString("dd-MM-yyyy").ExtChangeDateTimeFormat().ExtFinancialYear();
            var result = await _ctx.DbSalesInvoiceMaster.Where(w => w.invoice_code == code && w.financial_year == FinancialYear).FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}

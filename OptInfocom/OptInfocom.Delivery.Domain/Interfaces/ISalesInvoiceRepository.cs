using OptInfocom.Delivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Delivery.Domain.Interfaces
{
    public interface ISalesInvoiceRepository
    {
        Task<SalesInvoiceMaster> GetByInvoiceIDAsync(int id, CancellationToken cancellationToken = default);       
        Task<SalesInvoiceMaster> GetByInvoiceCodeAsync(string code, CancellationToken cancellationToken = default);       
    }
}

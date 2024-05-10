using OptInfocom.Delivery.Application.Interfaces;
using OptInfocom.Delivery.Domain.Interfaces;
using OptInfocom.Delivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Delivery.Application.Services
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ISalesInvoiceRepository _salesInvoiceRepository;
        public SalesInvoiceService(ISalesInvoiceRepository salesInvoiceRepository)
        {
            _salesInvoiceRepository = salesInvoiceRepository;
        }

        public async Task<SalesInvoiceMaster> GetByInvoiceIDAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _salesInvoiceRepository.GetByInvoiceIDAsync(id);
            return result;
        }

        public async Task<SalesInvoiceMaster> GetByInvoiceCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            var result = await _salesInvoiceRepository.GetByInvoiceCodeAsync(code);
            return result;
        }
    }
}

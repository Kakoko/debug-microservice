﻿using OptInfocom.Delivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Delivery.Application.Interfaces
{
    public interface IDeliveryStatusService
    {
        Task<IEnumerable<DeliveryStatus>> GetByInvoiceIDAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> SaveAsync(DeliveryStatus entity, CancellationToken cancellationToken);
    }
}

using OptInfocom.Delivery.Application.Interfaces;
using OptInfocom.Delivery.Domain.Interfaces;
using OptInfocom.Delivery.Domain.Models;

namespace OptInfocom.Delivery.Application.Services
{
    public class DeliveryStatusService : IDeliveryStatusService
    {
        private readonly IDeliveryStatusRepository _deliveryStatusRepository;
        public DeliveryStatusService(IDeliveryStatusRepository deliveryStatusRepository)
        {
            _deliveryStatusRepository = deliveryStatusRepository;
        }
        public async Task<IEnumerable<DeliveryStatus>> GetByInvoiceIDAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _deliveryStatusRepository.GetByInvoiceIDAsync(id);
            return result;
        }

        public async Task<bool> SaveAsync(DeliveryStatus entity, CancellationToken cancellationToken)
        {
            var result = await _deliveryStatusRepository.SaveAsync(entity, cancellationToken);
            return result;
        }
    }
}

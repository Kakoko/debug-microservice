using OptInfocom.Item.Domain.Models;

namespace OptInfocom.Item.Application.Interfaces
{
    public interface IItemService
    {
        Task<ItemMaster> GetByIDAsync(int id, CancellationToken cancellationToken = default);
        Task<List<ItemMaster>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<List<ItemMaster>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}

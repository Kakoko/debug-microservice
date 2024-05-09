using OptInfocom.Item.Application.Interfaces;
using OptInfocom.Item.Domain.Interfaces;
using OptInfocom.Item.Domain.Models;

namespace OptInfocom.Item.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<List<ItemMaster>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _itemRepository.GetAllAsync(cancellationToken);
            return result;
        }

        public async Task<ItemMaster> GetByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _itemRepository.GetByIDAsync(id, cancellationToken);
            return result;
        }

        public async Task<List<ItemMaster>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var result = await _itemRepository.GetByNameAsync(name, cancellationToken);
            return result;
        }
    }
}

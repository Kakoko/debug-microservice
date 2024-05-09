using Microsoft.EntityFrameworkCore;
using OptInfocom.Item.Data.Context;
using OptInfocom.Item.Domain.Interfaces;
using OptInfocom.Item.Domain.Models;

namespace OptInfocom.Item.Data.Repository
{
    public class ItemRepository : IItemRepository
    {
        private ItemDbContext _ctx;
        public ItemRepository(ItemDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<ItemMaster>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _ctx.DbItemMaster.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<ItemMaster> GetByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _ctx.DbItemMaster.FindAsync(id, cancellationToken);
            return result;
        }

        public async Task<List<ItemMaster>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var result = await _ctx.DbItemMaster.Where(w => w.item_name.Contains(name)).ToListAsync(cancellationToken);
            return result;
        }
    }
}

using Inventory.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Data.Repository;

public class InventoryItemRepository(AppDbcontext dbContext) : IInventoryItemRepository
{
    public async Task<bool> Create(InventoryItem inventoryItem)
    {
        await dbContext.AddAsync(inventoryItem);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public Task<bool> Delete(Guid inventoryItemId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<InventoryItem>> GetAll()
    {
        return await dbContext.InventorieItems.ToListAsync();
    }

    public Task<InventoryItem?> GetByProductId(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(InventoryItem inventoryItem)
    {
        throw new NotImplementedException();
    }
}

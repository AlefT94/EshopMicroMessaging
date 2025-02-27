using Inventory.API.Entities;

namespace Inventory.API.Data.Repository;

public interface IInventoryItemRepository
{
    public Task<bool> Create(InventoryItem inventoryItem);
    public Task<bool> Delete(Guid inventoryItemId);
    public Task<InventoryItem?> GetByProductId(Guid productId);
    public Task<IEnumerable<InventoryItem>> GetAll();
    public Task<bool> Update(InventoryItem inventoryItem);
}

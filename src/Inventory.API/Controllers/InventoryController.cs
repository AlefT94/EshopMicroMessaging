using Inventory.API.Data.Repository;
using Inventory.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InventoryController(IInventoryItemRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<InventoryItem>> GetAll()
    {
        return await repository.GetAll();
    }
}

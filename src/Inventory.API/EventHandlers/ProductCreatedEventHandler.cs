using Inventory.API.Data.Repository;
using Inventory.API.Entities;
using MassTransit;
using Shared.Library.Events.Products;

namespace Inventory.API.EventHandlers;

public class ProductCreatedEventHandler : IConsumer<ProductCreated>
{
    private readonly IInventoryItemRepository _repository;

    public ProductCreatedEventHandler(IInventoryItemRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        var message = context.Message;
        InventoryItem newInventoryItem = InventoryItem.Create(message.Id, message.Name);
        await _repository.Create(newInventoryItem);
    }

}

namespace Shared.Library.Events.Products;
public record ProductCreated(Guid Id, string Name, string Description, decimal Price );
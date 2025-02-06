namespace Shared.Library.Events.Products;
public record ProductUpdated(Guid Id, string Name, string Description, decimal Price);

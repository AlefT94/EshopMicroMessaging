using Products.API.Entities;

namespace Products.API.Services;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken);
    public Task<Product> GetProductById(Guid Id, CancellationToken cancellationToken);
    public Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken);
    public Task<bool> DeleteProduct(Guid Id, CancellationToken cancellationToken);
    public Task<bool> CreateProduct(Product product, CancellationToken cancellationToken);
}

using Products.API.Entities;

namespace Products.API.Data.Repository;

public interface IProductRepository
{
    public Task<bool> Create(Product newProduct);
    public Task<bool> Delete(Product product);
    public Task<Product?> GetById(Guid id);
    public Task<IEnumerable<Product>> GetAll();
    public Task<bool> Update(Product product);
}

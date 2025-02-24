using Microsoft.EntityFrameworkCore;
using Products.API.Entities;

namespace Products.API.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbcontext _dbContext;

    public ProductRepository(AppDbcontext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> Create(Product newProduct)
    {
        await _dbContext.AddAsync(newProduct);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Product product)
    {
        _dbContext.Remove(product);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _dbContext.Products.FindAsync(id);
    }

    public async Task<bool> Update(Product product)
    {
        Product dbProduct = await _dbContext.Products.FindAsync(product.Id);
        dbProduct.Name = product.Name;
        dbProduct.Description = product.Description;
        dbProduct.Price = product.Price;

        return await _dbContext.SaveChangesAsync() > 0;
    }
}

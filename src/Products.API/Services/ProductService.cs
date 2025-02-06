using Microsoft.EntityFrameworkCore;
using Products.API.Bus;
using Products.API.Data;
using Products.API.Entities;
using Shared.Library.Events.Products;

namespace Products.API.Services;

public class ProductService(AppDbcontext _context, IBusService _bus) : IProductService
{
    public async Task<bool> CreateProduct(Product product, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            Description = product.Description,
            Name = product.Name,
            Price = product.Price,
        };
        await _context.AddAsync(newProduct);
        await _context.SaveChangesAsync();

        _bus.Publish<ProductCreated>(new ProductCreated(newProduct.Id, newProduct.Name, newProduct.Description, newProduct.Price), new CancellationToken());

        return true;
    }

    public async Task<bool> DeleteProduct(Guid Id, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(Id);
        if(product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        _bus.Publish<ProductDeleted>(new ProductDeleted(Id), new CancellationToken());

        return true;
    }

    public async Task<Product> GetProductById(Guid Id, CancellationToken cancellationToken)
    {
        return await _context.Products.FindAsync(Id);
    }

    public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken)
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        var productDb = await _context.Products.FindAsync(product.Id);
        if(productDb == null)
        {
            return false;
        }

        productDb.Name = product.Name;
        productDb.Description = product.Description;
        productDb.Price = product.Price;

        await _context.SaveChangesAsync();

        _bus.Publish<ProductUpdated>(new ProductUpdated(productDb.Id, productDb.Name, productDb.Description, productDb.Price), new CancellationToken());

        return true;
    }
}

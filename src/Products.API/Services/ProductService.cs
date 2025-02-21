using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Products.API.Bus;
using Products.API.Data;
using Products.API.Data.Repositoty;
using Products.API.Entities;
using Shared.Library.Events.Products;

namespace Products.API.Services;

public class ProductService(IProductRepository _repository, IBusService _bus) : IProductService
{
    public async Task<bool> CreateProduct(Product product, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            Description = product.Description,
            Name = product.Name,
            Price = product.Price,
        };
        var result = await _repository.Create(newProduct);

        await _bus.Publish<ProductCreated>(new ProductCreated(newProduct.Id, newProduct.Name, newProduct.Description, newProduct.Price), new CancellationToken());

        return result;
    }

    public async Task<bool> DeleteProduct(Guid Id, CancellationToken cancellationToken)
    {
        var product = await _repository.GetById(Id);
        if(product == null)
        {
            return false;
        }

        var result = await _repository.Delete(product);

        await _bus.Publish<ProductDeleted>(new ProductDeleted(Id), new CancellationToken());

        return result;
    }

    public async Task<Product> GetProductById(Guid Id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(Id);
    }

    public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken)
    {
        return await _repository.GetAll();
    }

    public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        var productDb = await _repository.GetById(product.Id);
        if(productDb == null)
        {
            return false;
        }

        productDb.Name = product.Name;
        productDb.Description = product.Description;
        productDb.Price = product.Price;

        var result = await _repository.Update(productDb);

        await _bus.Publish<ProductUpdated>(new ProductUpdated(productDb.Id, productDb.Name, productDb.Description, productDb.Price), new CancellationToken());

        return result;
    }
}

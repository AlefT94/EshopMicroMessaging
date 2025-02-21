using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Products.API.Bus;
using Products.API.Data;
using Products.API.Data.Repositoty;
using Products.API.Services;

namespace Product.API.Test.Services;
public class ProductServiceTests
{
    private readonly IProductRepository _repository;
    private readonly IBusService _bus;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _repository = Substitute.For<IProductRepository>();
        _bus = Substitute.For<IBusService>();
        _productService = new ProductService(_repository, _bus);
    }

    [Fact]
    public async Task GivenProductDataIsOk_WhenCreateProductIsCalled_ThenReturnTrue()
    {
        //Arrange
        Products.API.Entities.Product newProduct = new Products.API.Entities.Product()
        {
            Description = "description",
            Name = "name",
            Price = 10.53M
        };

        _repository.Create(Arg.Any<Products.API.Entities.Product>()).Returns(true);

        //Act
        var result = await _productService.CreateProduct(newProduct, new CancellationToken());

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GivenProductDataIsNotOk_WhenCreateProductIsCalled_ThenReturnFalse()
    {
        //Arrange
        Products.API.Entities.Product newProduct = new Products.API.Entities.Product()
        {
            Description = "description",
            Name = "name",
            Price = 10.53M
        };

        _repository.Create(Arg.Any<Products.API.Entities.Product>()).Returns(false);

        //Act
        var result = await _productService.CreateProduct(newProduct, new CancellationToken());

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GivenProductExists_WhenDeleteProductIsCalled_ThenReturnTrue()
    {
        //Arrange
        Guid id = new Guid();

        Products.API.Entities.Product dbProduct = new()
        {
            Id = id,
            Name = "name",
            Description = "description",
            Price = 10.53M
        };

        _repository.GetById(Arg.Any<Guid>()).Returns(dbProduct);
        _repository.Delete(Arg.Any<Products.API.Entities.Product>()).Returns(true);

        //Act
        var result = await _productService.DeleteProduct(id, new CancellationToken());

        //Assert
        Assert.True(result);
        await _repository.Received(1).GetById(id);
        await _repository.Received(1).Delete(Arg.Any<Products.API.Entities.Product>());
    }

    [Fact]
    public async Task GivenProductNotExists_WhenDeleteProductIsCalled_ThenReturnFalse()
    {
        //Arrange
        Guid id = Guid.NewGuid();

        Products.API.Entities.Product dbProduct = new()
        {
            Id = id,
            Name = "name",
            Description = "description",
            Price = 10.53M
        };

        _repository.GetById(Arg.Any<Guid>()).ReturnsNull();

        //Act
        var result = await _productService.DeleteProduct(id, new CancellationToken());

        //Assert
        Assert.False(result);
        await _repository.Received(1).GetById(id);
        await _repository.DidNotReceive().Delete(Arg.Any<Products.API.Entities.Product>());
    }
}

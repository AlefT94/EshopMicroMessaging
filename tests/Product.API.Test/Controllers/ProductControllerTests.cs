using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Products.API.Controllers;
using Products.API.Services;

namespace Product.API.Test.Controllers;
public class ProductControllerTests
{
    private readonly IProductService _productServiceMock;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _productServiceMock = Substitute.For<IProductService>();
        _controller = new ProductController(_productServiceMock);
    }

    [Fact]
    public async Task GivenProductExists_WhenGetIsCalled_ThenReturnOkWithProducts()
    {
        //Arrange
        var products = new List<Products.API.Entities.Product>
        {
            new(){Id = new Guid(), Name="Test Product", Description="Description Test Produc", Price = 10.53M}
        };
        _productServiceMock.GetProducts(Arg.Any<CancellationToken>()).Returns(products);
        
        //Act
        var result = await _controller.Get(CancellationToken.None);

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GivenProductDoesNotExists_WhenGetIsCalled_ThenOkWithNoProducts()
    {
        //Arrange

        _productServiceMock.GetProducts(Arg.Any<CancellationToken>()).ReturnsNull();

        //Act
        var result = await _controller.Get(CancellationToken.None);

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GivenProductExists_WhenGetByIdIsCalled_ThenReturnOkWithProduct()
    {
        //Arrange
        Products.API.Entities.Product product = new() { Id = new Guid(), Name = "Test Product", Description = "Description Test Produc", Price = 10.53M };

        _productServiceMock.GetProductById(Arg.Any <Guid>() ,Arg.Any<CancellationToken>()).Returns(product);

        //Act
        var result = await _controller.GetById(new Guid(),CancellationToken.None);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProduct = Assert.IsType<Products.API.Entities.Product>(okResult.Value);
        Assert.Equal(product.Id, returnedProduct.Id);
        Assert.Equal(product.Name, returnedProduct.Name);
    }

    [Fact]
    public async Task GivenProductDoesNotExists_WhenGetByIdIsCalled_ThenReturnNotFound()
    {
        //Arrange
        _productServiceMock.GetProductById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();

        //Act
        var result = await _controller.GetById(new Guid(), CancellationToken.None);

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GivenNewProductDataIsOk_WhenPostIsCalled_ThenReturnNoContent()
    {
        //Arrange
        _productServiceMock.CreateProduct(Arg.Any<Products.API.Entities.Product>(), Arg.Any<CancellationToken>()).Returns(true);
        Products.API.Entities.Product product = new() { Id = new Guid(), Name = "Test Product", Description = "Description Test Produc", Price = 10.53M };

        //Act
        var result = await _controller.Post(product, CancellationToken.None);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GivenNewProductDataIsNotOk_WhenPostIsCalled_ThenReturnBadRequest()
    {
        //Arrange
        _productServiceMock.CreateProduct(Arg.Any<Products.API.Entities.Product>(), Arg.Any<CancellationToken>()).Returns(false);
        Products.API.Entities.Product product = new() { Id = new Guid(), Name = "Test Product", Description = "Description Test Produc", Price = 10.53M };

        //Act
        var result = await _controller.Post(product, CancellationToken.None);

        //Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GivenProductDataIsOk_WhenPutIsCalled_ThenReturnNoContent()
    {
        //Arrange
        _productServiceMock.UpdateProduct(Arg.Any<Products.API.Entities.Product>(), Arg.Any<CancellationToken>()).Returns(true);
        Products.API.Entities.Product product = new() { Id = new Guid(), Name = "Test Product", Description = "Description Test Produc", Price = 10.53M };

        //Act
        var result = await _controller.Put(product, CancellationToken.None);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GivenProductDataIsNotOk_WhenPutIsCalled_ThenReturnNotFound()
    {
        //Arrange
        _productServiceMock.UpdateProduct(Arg.Any<Products.API.Entities.Product>(), Arg.Any<CancellationToken>()).Returns(false);
        Products.API.Entities.Product product = new() { Id = new Guid(), Name = "Test Product", Description = "Description Test Produc", Price = 10.53M };

        //Act
        var result = await _controller.Put(product, CancellationToken.None);

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GivenProductExists_WhenDeleteIsCalled_ThenReturnNoContent()
    {
        //Arrange
        _productServiceMock.DeleteProduct(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);

        //Act
        var result = await _controller.Delete(new Guid(), CancellationToken.None);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GivenProductExists_WhenDeleteIsCalled_ThenReturnNotfound()
    {
        //Arrange
        _productServiceMock.DeleteProduct(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await _controller.Delete(new Guid(), CancellationToken.None);

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }
}

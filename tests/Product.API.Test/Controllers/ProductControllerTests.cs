using NSubstitute;
using Products.API.Services;
using Products.API.Entities;
using Products.API.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ReturnsExtensions;

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
    public async Task GivenProductDoesNotExists_WhenGetIsCalled_ThenReturnNotfound()
    {
        //Arrange

        _productServiceMock.GetProducts(Arg.Any<CancellationToken>()).ReturnsNull();

        //Act
        var result = await _controller.Get(CancellationToken.None);

        //Assert
        Assert.IsType<NotFoundResult>(result);
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
        Assert.IsType<OkObjectResult>(result);
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
}

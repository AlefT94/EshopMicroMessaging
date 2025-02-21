using NSubstitute;
using Products.API.Bus;
using Products.API.Data;
using Products.API.Services;

namespace Product.API.Test.Services;
public class ProductServiceTests
{
    private readonly AppDbcontext _dbContext;
    private readonly IBusService _bus;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _dbContext = Substitute.For<AppDbcontext>();
        _bus = Substitute.For<IBusService>();
        _productService = new ProductService(_dbContext, _bus);
    }


}

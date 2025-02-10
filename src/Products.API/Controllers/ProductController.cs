using Microsoft.AspNetCore.Mvc;
using Products.API.Entities;
using Products.API.Services;

namespace Products.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService _productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var products = await _productService.GetProducts(cancellationToken);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductById(id, cancellationToken);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product, CancellationToken cancellationToken)
    {
        var success = await _productService.CreateProduct(product, cancellationToken);
        if (!success)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Product product, CancellationToken cancellationToken)
    {
        var success = await _productService.UpdateProduct(product, cancellationToken);

        if(!success)
            return NotFound();


        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var success = await _productService.DeleteProduct(id, cancellationToken);

        if (!success)
            return NotFound();

        return NoContent();
    }
}

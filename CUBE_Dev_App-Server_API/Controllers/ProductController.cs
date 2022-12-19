using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    public ProductController()
    {
    }

    [HttpGet]
    public ActionResult<List<Product>> GetAll() =>
        ProductService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = ProductService.Get(id);

        if (product is null)
            return NotFound();

        return product;
    }

    [HttpPost]
    public IActionResult Post(Product product)
    {
        ProductService.Add(product);

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Product product)
    {
        if (id != product.PkProduct)
            return BadRequest();

        var existingProduct = ProductService.Get(id);

        if (existingProduct is null)
            return NotFound();

        ProductService.Update(product);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = ProductService.Get(id);

        if (product is null)
            return NotFound();

        ProductService.Delete(id);

        return NoContent();
    }
}

﻿using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        if (!ProductService.GetAll(out List<Product> products))
            return StatusCode(500);
        
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!ProductService.Get(id, out Product? product))
            return StatusCode(500);

        if (product is null)
            return NotFound();
        
        return Ok(product);
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (!ProductService.Add(product))
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { pkProduct = product.PkProduct }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product product)
    {
        if (id != product.PkProduct)
            return BadRequest();

        if (!ProductService.Get(id, out Product? existingProduct))
            return StatusCode(500);

        if (existingProduct is null)
            return NotFound();

        if (!ProductService.Update(product))
            return StatusCode(500);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!ProductService.Get(id, out Product? product))
            return StatusCode(500);

        if (product is null)
            return NotFound();

        if (!ProductService.Delete(id))
            return StatusCode(500);

        return NoContent();
    }
}

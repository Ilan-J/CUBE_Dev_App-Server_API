using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    public SupplierController()
    {
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        if (!SupplierService.GetAll(out List<Supplier> suppliers))
            return StatusCode(500);

        return Ok(suppliers);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!SupplierService.Get(id, out Supplier? supplier))
            return StatusCode(500);

        if (supplier is null)
            return NotFound();

        return Ok(supplier);
    }

    [HttpPost]
    public IActionResult Create(Supplier supplier)
    {
        if (!SupplierService.Add(supplier))
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { pkSupplier = supplier.PkSupplier }, supplier);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Supplier supplier)
    {
        if (id != supplier.PkSupplier)
            return BadRequest();
        
        if (!SupplierService.Get(id, out Supplier? existingSupplier))
            return StatusCode(500);

        if (existingSupplier is null)
            return NotFound();
        
        if (!SupplierService.Update(supplier))
            return StatusCode(500);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!SupplierService.Get(id, out Supplier? supplier))
            return StatusCode(500);
        
        if (supplier is null)
            return NotFound();

        if (!SupplierService.Delete(id))
            return StatusCode(500);

        return NoContent();
    }
}

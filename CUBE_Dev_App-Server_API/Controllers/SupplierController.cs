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
    public ActionResult<List<Supplier>> GetAll() =>
        SupplierService.GetAll();
    
    [HttpGet("{id}")]
    public ActionResult<Supplier> Get(int id)
    {
        var supplier = SupplierService.Get(id);

        if (supplier is null)
            return NotFound();

        return supplier;
    }

    [HttpPost]
    public IActionResult Create(Supplier supplier)
    {
        SupplierService.Add(supplier);

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Supplier supplier)
    {
        if (id != supplier.PkSupplier)
            return BadRequest();
        
        var existingSupplier = SupplierService.Get(id);

        if (existingSupplier is null)
            return NotFound();
        
        SupplierService.Update(supplier);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var supplier = SupplierService.Get(id);
        
        if (supplier is null)
            return NotFound();

        SupplierService.Delete(id);

        return NoContent();
    }
}

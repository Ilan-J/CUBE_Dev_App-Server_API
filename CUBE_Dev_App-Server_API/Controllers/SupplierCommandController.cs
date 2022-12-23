using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierCommandController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        if (!SupplierCommandService.GetAll(out List<SupplierCommand> supplierCommands))
            return StatusCode(500);
        
        return Ok(supplierCommands);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!SupplierCommandService.Get(id, out SupplierCommand? supplierCommand))
            return StatusCode(500);

        if (supplierCommand is null)
            return NotFound();

        return Ok(supplierCommand);
    }

    [HttpPost]
    public IActionResult Create(SupplierCommand supplierCommand)
    {
        if (!SupplierCommandService.Add(supplierCommand))
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { pkSupplierCommand = supplierCommand.PkSupplierCommand }, supplierCommand);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, SupplierCommand supplierCommand)
    {
        if (id != supplierCommand.PkSupplierCommand)
            return BadRequest();

        if (!SupplierCommandService.Get(id, out SupplierCommand? ExistingSupplierCommand))
            return StatusCode(500);

        if (ExistingSupplierCommand is null)
            return NotFound();

        if (!SupplierCommandService.Update(supplierCommand))
            return StatusCode(500);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!SupplierCommandService.Get(id, out SupplierCommand? supplierCommand))
            return StatusCode(500);

        if (supplierCommand is null)
            return NotFound();

        if (!SupplierCommandService.Delete(id))
            return StatusCode(500);

        return NoContent();
    }
}

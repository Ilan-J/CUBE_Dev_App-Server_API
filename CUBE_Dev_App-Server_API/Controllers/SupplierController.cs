using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

/// <summary>
/// Every requests related to suppliers
/// </summary>
[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    /// <summary>
    /// Gets the list of every suppliers
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        if (!SupplierService.GetAll(out List<Supplier>? suppliers))
            return StatusCode(500);

        return Ok(suppliers);
    }

    /// <summary>
    /// Gets the data of a specified supplier
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!SupplierService.Get(id, out Supplier? supplier))
            return StatusCode(500);

        if (supplier == null)
            return NotFound();

        return Ok(supplier);
    }

    /// <summary>
    /// Gets the list of every supplier names, and their IDs
    /// </summary>
    [HttpGet("Names")]
    public IActionResult GetNames()
    {
        if (!SupplierService.GetAllNames(out List<SupplierName>? supplierNames))
            return StatusCode(500);

        return Ok(supplierNames);
    }

    /// <summary>
    /// Creates a new supplier in the database
    /// </summary>
    /// <remarks>
    /// Sample request :
    ///
    ///     POST
    ///     {
    ///         "name": "string",
    ///         "email": "string",
    ///         "address": "string",
    ///         "town": "string",
    ///         "postalCode": "string",
    ///         "country": "string",
    ///     }
    /// </remarks>
    [HttpPost]
    public IActionResult Create(Supplier supplier)
    {
        if (!SupplierService.Add(supplier))
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { pkSupplier = supplier.IDSupplier }, supplier);
    }

    /// <summary>
    /// Updates a specified supplier
    /// </summary>
    /// <remarks>
    /// Sample request :
    ///
    ///     PUT
    ///     int id,
    ///     {
    ///         "idSupplier", 0,
    ///         "name": "string",
    ///         "email": "string",
    ///         "address": "string",
    ///         "town": "string",
    ///         "postalCode": "string",
    ///         "country": "string",
    ///     }
    /// </remarks>
    [HttpPut("{id}")]
    public IActionResult Update(int id, Supplier supplier)
    {
        if (id != supplier.IDSupplier)
            return BadRequest();

        if (!SupplierService.Get(id, out Supplier? existingSupplier))
            return StatusCode(500);

        if (existingSupplier == null)
            return NotFound();

        if (!SupplierService.Update(supplier))
            return StatusCode(500);

        return NoContent();
    }
}

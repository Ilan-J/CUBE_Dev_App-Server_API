using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

/// <summary>
/// Every requests related to supplier commands
/// </summary>
[ApiController]
[Route("[controller]")]
public class SupplierCommandController : ControllerBase
{
    /// <summary>
    /// Gets the lists of every supplier command header
    /// </summary>
    [HttpGet("Headers")]
    public IActionResult GetAllHeaders()
    {
        if (!SupplierCommandService.GetAllHeaders(out List<SupplierCommand> supplierCommands))
            return StatusCode(500);

        return Ok(supplierCommands);
    }

    /// <summary>
    /// Gets the data of a specified supplier command
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!SupplierCommandService.Get(id, out SupplierCommand? supplierCommand))
            return StatusCode(500);

        if (supplierCommand == null)
            return NotFound();

        return Ok(supplierCommand);
    }

    /// <summary>
    /// Creates a new supplier command in the database
    /// </summary>
    /// <remarks>
    /// Sample request :
    ///
    ///     POST
    ///     SupplierCommand
    ///     {
    ///         "commandDate": "2023-02-13T09:13:18.590Z",
    ///         "idSupplier": 1,
    ///         "idCommandStatus": 2,
    ///         "supplierCommandList": [
    ///             {
    ///                 "idArticle": 1,
    ///                 "quantity": 10,
    ///                 "idQuantityType": 1
    ///             }
    ///         ]
    ///     }
    /// </remarks>
    [HttpPost]
    public IActionResult Create(SupplierCommand supplierCommand)
    {
        if (!SupplierCommandService.Add(supplierCommand))
            return StatusCode(500);

        int id = DBConnection.GetHighestPrimaryKey("SupplierCommand");

        if (!SupplierCommandService.Get(id, out SupplierCommand? supplierCommandCreated))
            return StatusCode(500);

        if (supplierCommandCreated == null)
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { supplierCommandCreated.IDSupplierCommand }, supplierCommandCreated);
    }

    /// <summary>
    /// Closes the specified supplier command
    /// </summary>
    [HttpPut("Close")]
    public IActionResult Close(int id)
    {
        if (!SupplierCommandService.Get(id, out SupplierCommand? supplierCommand))
            return StatusCode(500);

        if (supplierCommand == null)
            return NotFound();

        if (supplierCommand.IDCommandStatus == 1)
            return BadRequest("Command already closed !");

        if (!SupplierCommandService.Close(supplierCommand))
            return StatusCode(500);

        return Ok();
    }

    /// <summary>
    /// Cancels the specified supplier command
    /// </summary>
    [HttpPut("Cancel")]
    public IActionResult Cancel(int id)
    {
        if (!SupplierCommandService.Get(id, out SupplierCommand? supplierCommand))
            return StatusCode(500);

        if (supplierCommand == null)
            return NotFound();

        if (supplierCommand.IDCommandStatus == 1)
            return BadRequest("Command already closed !");

        if (!SupplierCommandService.Cancel(supplierCommand))
            return StatusCode(500);

        return Ok();
    }
}

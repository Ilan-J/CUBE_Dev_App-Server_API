using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

/// <summary>
/// Every requests related to client commands
/// </summary>
[ApiController]
[Route("[controller]")]
public class ClientCommandController : ControllerBase
{
    /// <summary>
    /// Gets the lists of every client command header
    /// </summary>
    [HttpGet("Headers")]
    public IActionResult GetAll()
    {
        if (!ClientCommandService.GetAllHeaders(out List<ClientCommand> clientCommands))
            return StatusCode(500);

        return Ok(clientCommands);
    }

    /// <summary>
    /// Gets the data of a specified client command
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!ClientCommandService.Get(id, out ClientCommand? clientCommand))
            return StatusCode(500);

        if (clientCommand == null)
            return NotFound();

        return Ok(clientCommand);
    }

    /// <summary>
    /// Creates a new client command in the database
    /// </summary>
    /// <remarks>
    /// Sample request :
    ///
    ///     POST
    ///     ClientCommand
    ///     {
    ///         "commandDate": "2023-02-13T09:13:18.590Z",
    ///         "idClient": 1,
    ///         "idCommandStatus": 2,
    ///         "clientCommandsList": [
    ///             {
    ///                 "idArticle": 1,
    ///                 "quantity": 10,
    ///                 "idQuantityType": 1
    ///             }
    ///         ]
    ///     }
    /// </remarks>
    [HttpPost()]
    public IActionResult Create(ClientCommand clientCommand)
    {
        if (!ClientCommandService.Add(clientCommand))
            return StatusCode(500);

        int highestPrimaryKey = DBConnection.GetHighestPrimaryKey("ClientCommand");

        if (!ClientCommandService.Get(highestPrimaryKey, out ClientCommand? createdClientCommand))
            return StatusCode(500);

        if (createdClientCommand == null)
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { createdClientCommand.IDClientCommand }, createdClientCommand);
    }

    /// <summary>
    /// Closes the specified client command
    /// </summary>
    [HttpPut("Close")]
    public IActionResult Close(int id)
    {
        if (!ClientCommandService.Get(id, out ClientCommand? supplierCommand))
            return StatusCode(500);

        if (supplierCommand == null)
            return NotFound();

        if (supplierCommand.IDCommandStatus == 1)
            return BadRequest("Command already closed !");

        if (!ClientCommandService.Close(supplierCommand))
            return StatusCode(500);

        return Ok();
    }

    /// <summary>
    /// Cancels the specified client command
    /// </summary>
    [HttpPut("Cancel")]
    public IActionResult Cancel(int id)
    {
        if (!ClientCommandService.Get(id, out ClientCommand? supplierCommand))
            return StatusCode(500);

        if (supplierCommand == null)
            return NotFound();

        if (supplierCommand.IDCommandStatus == 1)
            return BadRequest("Command already closed !");

        if (!ClientCommandService.Cancel(supplierCommand))
            return StatusCode(500);

        return Ok();
    }
}

using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientCommandController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        if (!ClientCommandService.GetAll(out List<ClientCommand> clientCommands))
            return StatusCode(500);

        return Ok(clientCommands);

        /*if (!ClientService.GetAll(out List<Client> clients))
            return false;

        clientCommands.ForEach(clientCommand =>
        {
            clientCommand.Client = clients[clientCommand.Client.PkClient - 1];
        });*/
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!ClientCommandService.Get(id, out ClientCommand? clientCommand))
            return StatusCode(500);

        if (clientCommand is null)
            return NotFound();

        return Ok(clientCommand);

        /*ClientService.Get(clientCommand.Client.PkClient, out Client? client);
        clientCommand.Client = client;*/
    }

    [HttpPost]
    public IActionResult Create(ClientCommand clientCommand)
    {
        if (!ClientCommandService.Add(clientCommand))
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { pkClientCommand = clientCommand.IDClientCommand }, clientCommand);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, ClientCommand clientCommand)
    {
        if (id != clientCommand.IDClientCommand)
            return BadRequest();

        if (!ClientCommandService.Get(id, out ClientCommand? existingClientCommand))
            return StatusCode(500);

        if (existingClientCommand is null)
            return NotFound();

        if (!ClientCommandService.Update(clientCommand))
            return StatusCode(500);

        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        if (!ClientCommandService.Get(id, out ClientCommand? clientCommand))
            return StatusCode(500);

        if (clientCommand is null)
            return NotFound();

        if (!ClientCommandService.Delete(id))
            return StatusCode(500);

        return NoContent();
    }
}

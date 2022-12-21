using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

[ApiController]
[Route("[controller]")]
public class WineFamilyController : ControllerBase
{
    public WineFamilyController()
    {
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        if (!WineFamilyService.GetAll(out List<WineFamily> wineFamilies))
            return StatusCode(500);

        return Ok(wineFamilies);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!WineFamilyService.Get(id, out WineFamily? wineFamily))
            return StatusCode(500);

        if (wineFamily is null)
            return NotFound();

        return Ok(wineFamily);
    }

    [HttpPost]
    public IActionResult Create(WineFamily wineFamily)
    {
        if (!WineFamilyService.Add(wineFamily))
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { pkWineFamily = wineFamily.PkWineFamily }, wineFamily);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, WineFamily wineFamily)
    {
        if (id != wineFamily.PkWineFamily)
            return BadRequest();

        if (!WineFamilyService.Get(id, out WineFamily? existingWineFamily))
            return StatusCode(500);

        if (existingWineFamily is null)
            return NotFound();

        if (!WineFamilyService.Update(wineFamily))
            return StatusCode(500);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!WineFamilyService.Get(id, out WineFamily? wineFamily))
            return StatusCode(500);

        if (wineFamily is null)
            return NotFound();

        if (!WineFamilyService.Delete(id))
            return StatusCode(500);

        return NoContent();
    }
}

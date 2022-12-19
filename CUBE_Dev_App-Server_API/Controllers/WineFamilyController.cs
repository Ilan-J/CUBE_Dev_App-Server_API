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
    public ActionResult<List<WineFamily>> GetAll() =>
        WineFamilyService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<WineFamily> Get(int id)
    {
        var wineFamily = WineFamilyService.Get(id);

        if (wineFamily is null)
            return NotFound();

        return wineFamily;
    }

    [HttpPost]
    public IActionResult Create(WineFamily wineFamily)
    {
        WineFamilyService.Add(wineFamily);

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, WineFamily wineFamily)
    {
        if (id != wineFamily.PkWineFamily)
            return BadRequest();

        var existingWineFamily = WineFamilyService.Get(id);

        if (existingWineFamily is null)
            return NotFound();

        WineFamilyService.Update(wineFamily);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var wineFamily = WineFamilyService.Get(id);

        if (wineFamily is null)
            return NotFound();

        WineFamilyService.Delete(id);

        return NoContent();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class HojasDeVidaController : ControllerBase
{
    // GET
    [HttpGet("mi-cv")]
    [Authorize(Roles = "Candidato")]
    public IActionResult GetMiHojaDeVida()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        return Ok(new { Mensaje = "Hoja de vida obtenida exitosamente", UserId = userId });
    }

    // POST
    [HttpPost]
    [Authorize(Roles = "Candidato")]
    public IActionResult GuardarHojaDeVida([FromBody] object datosCv)
    {
        
        return Ok(new { Mensaje = "Hoja de vida guardada correctamente." });
    }
}
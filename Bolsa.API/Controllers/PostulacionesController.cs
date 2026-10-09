using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostulacionesController : ControllerBase
{
    // POST
    [HttpPost]
    [Authorize(Roles = "Candidato")]
    public IActionResult CrearPostulacion([FromBody] PostulacionDto modelo)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { mensaje = "Usuario no autenticado." });
        }

       
        return Ok(new { mensaje = "Postulación registrada exitosamente.", candidatoId = userId, plazaId = modelo.IdPlaza });
    }

    // GET
    [HttpGet]
    [Authorize(Roles = "Administrador,Agente de Selección")]
    public IActionResult GetPostulaciones()
    {
        
        return Ok(new List<object>());
    }
}

public class PostulacionDto
{
    public int IdPlaza { get; set; }
}
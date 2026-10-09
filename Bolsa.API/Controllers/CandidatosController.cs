using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CandidatosController : ControllerBase
{
    // PUT
    [HttpPut("perfil")]
    [Authorize(Roles = "Candidato")]
    public IActionResult ActualizarPerfil([FromBody] PerfilCandidatoDto modelo)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        
        return Ok(new { mensaje = "Perfil y CV actualizados exitosamente.", usuarioId = userId });
    }

    // POST
    [HttpPost("aplicar")]
    [Authorize(Roles = "Candidato")]
    public IActionResult AplicarPlaza([FromBody] AplicacionDto modelo)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        
        return Ok(new { mensaje = "Postulación enviada correctamente a la plaza.", plazaId = modelo.IdPlaza });
    }
}

public class PerfilCandidatoDto
{
    public string NombreCompleto { get; set; } = string.Empty;
    public string ResumenProfesional { get; set; } = string.Empty;
    public string UrlCv { get; set; } = string.Empty; 
}

public class AplicacionDto
{
    public int IdPlaza { get; set; }
}
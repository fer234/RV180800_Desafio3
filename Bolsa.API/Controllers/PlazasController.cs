using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PlazasController : ControllerBase
{
    // GET: api/plazas
    [HttpGet]
    public IActionResult GetPlazas()
    {
        
        return Ok(new List<object>());
    }

    // POST
    [HttpPost]
    [Authorize(Roles = "Administrador,Agente de Selección")]
    public IActionResult CrearPlaza([FromBody] object nuevaPlaza)
    {
        return Ok(new { mensaje = "Plaza creada con éxito." });
    }

    // PATCH
    [HttpPatch("{id}/estado")]
    [Authorize(Roles = "Administrador,Agente de Selección")]
    public IActionResult CambiarEstado(int id, [FromBody] EstadoDto estadoDto)
    {
        
        return Ok(new { mensaje = $"Estado de la plaza {id} actualizado a {estadoDto.NuevoEstado}." });
    }
}

public class EstadoDto
{
    public string NuevoEstado { get; set; } = "Cerrada";
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Administrador")]
public class UsuariosController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsuariosController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _userManager.Users.Select(u => new {
            u.Id,
            u.UserName,
            u.Email
        }).ToListAsync();

        return Ok(usuarios);
    }

    // POST
    [HttpPost("asignar-rol")]
    public async Task<IActionResult> AsignarRol([FromBody] AsignarRolDto modelo)
    {
        var usuario = await _userManager.FindByIdAsync(modelo.UserId);
        if (usuario == null)
        {
            return NotFound(new { mensaje = "Usuario no encontrado." });
        }

        var resultado = await _userManager.AddToRoleAsync(usuario, modelo.Rol);
        if (resultado.Succeeded)
        {
            return Ok(new { mensaje = "Rol asignado correctamente." });
        }

        return BadRequest(resultado.Errors);
    }
}

public class AsignarRolDto
{
    public string UserId { get; set; }
    public string Rol { get; set; }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bolsa.DAL;
using Bolsa.Entities;
using Bolsa.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity configurado con ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Servicios de la capa BLL
builder.Services.AddScoped<PlazaService>();
builder.Services.AddScoped<PostulacionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// SEED DE ROLES Y USUARIOS
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Administrador", "Agente de Selección", "Candidato" };
    foreach (var rol in roles)
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            await roleManager.CreateAsync(new IdentityRole(rol));
        }
    }

    if (await userManager.FindByEmailAsync("admin@bolsa.sv") == null)
    {
        var admin = new ApplicationUser { UserName = "admin@bolsa.sv", Email = "admin@bolsa.sv", EmailConfirmed = true };
        await userManager.CreateAsync(admin, "Admin123*");
        await userManager.AddToRoleAsync(admin, "Administrador");
    }

    if (await userManager.FindByEmailAsync("agente@bolsa.sv") == null)
    {
        var agente = new ApplicationUser { UserName = "agente@bolsa.sv", Email = "agente@bolsa.sv", EmailConfirmed = true };
        await userManager.CreateAsync(agente, "Agente123*");
        await userManager.AddToRoleAsync(agente, "Agente de Selección");
    }

    if (await userManager.FindByEmailAsync("candidato@bolsa.sv") == null)
    {
        var candidato = new ApplicationUser { UserName = "candidato@bolsa.sv", Email = "candidato@bolsa.sv", EmailConfirmed = true };
        await userManager.CreateAsync(candidato, "Candidato123*");
        await userManager.AddToRoleAsync(candidato, "Candidato");
    }
}

app.Run();
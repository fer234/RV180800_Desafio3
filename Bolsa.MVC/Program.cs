using Microsoft.EntityFrameworkCore;
using Bolsa.DAL;
using Bolsa.Entities;
using Bolsa.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar conexión a SQL Server para MVC
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar TODOS los servicios de la capa BLL para que los CRUDs funcionen
builder.Services.AddScoped<PlazaService>();
builder.Services.AddScoped<PostulacionService>();
builder.Services.AddScoped<HojaDeVidaService>();

// Agregar soporte para controladores y vistas (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Sin autenticación ni autorización para que todo quede abierto y funcional
app.UseAuthorization();

// Ruta por defecto para controladores MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
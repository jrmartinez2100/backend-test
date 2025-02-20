using Application.IServices;
using Application.Services;
using Domain.IRepositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión del appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar el DbContext en el contenedor de dependencias
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add AutoMapper services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.
builder.Services.AddControllers();  // Ensure controllers are added
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar servicios de la aplicación
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IMarcaAutoRepository, MarcaAutoRepository>();
builder.Services.AddScoped<IMarcaAutoService, MarcaAutoService>();
// Add authentication and authorization services
builder.Services.AddAuthentication(); // If using authentication, configure it properly
builder.Services.AddAuthorization();
var app = builder.Build();

// Ejecutar migraciones automáticamente
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    try
    {
        dbContext.Database.Migrate(); // Aplica las migraciones pendientes
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error aplicando migraciones: {ex.Message}");
    }
}


// Configurar middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

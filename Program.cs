using CinemaAPP.Data;
using CinemaAPP.Repositories;
using CinemaAPP.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la conexión a la base de datos PostgreSQL
builder.Services.AddDbContext<CinemaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrar servicios y repositorios
builder.Services.AddScoped<PeliculaService>();
builder.Services.AddScoped<SalaCineService>();
builder.Services.AddScoped<IPeliculaSalaCineRepository, PeliculaSalaCineRepository>();
builder.Services.AddScoped<ISalaCineRepository, SalaCineRepository>();
builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>(); // Si tienes este repositorio también
builder.Services.AddScoped<PeliculaSalaCineService>();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.DateFormatString = "yyyy-MM-ddT"; // Si es necesario, ajusta el formato de fecha.
    });

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder => 
        builder.WithOrigins("http://localhost:4200")  // Ajusta esta URL según donde tengas el frontend
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); // Si estás usando cookies o autenticación, puedes permitir credenciales
});

var app = builder.Build();

// Usar la política CORS
app.UseCors("AllowFrontend");

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CinemaAPI v1"));
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PolizasCRUD.API.Data;
using PolizasCRUD.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de servicios
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<PolizaService>();
builder.Services.AddScoped<AuthService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173","http://localhost:54453", "http://localhost:5000")  // Frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PolizasCRUD API", Version = "v1" });
});

var app = builder.Build();

// Configure middlewares
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PolizasCRUD API v1"));
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
// Authentication and Authorization removed
app.MapControllers();

// Crear la base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al crear la base de datos.");
    }
}

app.Run();
using SansLimt.Api.Services;
using MongoDB.Driver;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// --- PUERTO (Render inyecta la variable PORT) ---
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

// --- CONFIGURACI�N DE MONGODB ---

var dbSection = builder.Configuration.GetSection("SansLimitDatabase");

var connectionString = dbSection.GetValue<string>("ConnectionString");
var databaseName = dbSection.GetValue<string>("DatabaseName");


if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("ERROR: No se encuentra 'ConnectionString' en appsettings.json. Revis� el nombre de la secci�n.");
}

var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase(databaseName);

// --- REGISTRO DE SERVICIOS ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyectamos la base de datos para que los servicios la usen
builder.Services.AddSingleton<IMongoDatabase>(mongoDatabase);

// Servicios
builder.Services.AddSingleton<ProductosService>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<CuponesService>();
builder.Services.AddSingleton<PedidosService>();

// --- CORS ---
var defaultOrigins = new[] { "http://localhost:5173", "http://localhost:5174" };
var extraOrigins = Environment.GetEnvironmentVariable("FRONTEND_URL")
    ?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    ?? Array.Empty<string>();
var allowedOrigins = defaultOrigins.Concat(extraOrigins).ToArray();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp", policy => {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (string.IsNullOrEmpty(port))
{
    app.UseHttpsRedirection();
}
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();
app.Run();
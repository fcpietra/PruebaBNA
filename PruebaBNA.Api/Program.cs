using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PruebaBNA.Application.Common.Interfaces;
using PruebaBNA.Application.Common.Settings;
using PruebaBNA.Application.Services;
using PruebaBNA.Infrastructure;
using PruebaBNA.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configurar Serilog (Consola + Archivo) ---
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// --- 2. Configurar Options Pattern ---
builder.Services.Configure<CacheSettings>(
    builder.Configuration.GetSection(CacheSettings.SectionName));

// --- 3. Inyectar Capas ---
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICuentaService, CuentaService>();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>()
    .AddUrlGroup(
        new Uri("https://api.bcra.gob.ar/centraldedeudores/v1.0/deudas/30500010912"),
        name: "BCRA API",
        timeout: TimeSpan.FromSeconds(5)
    );

var app = builder.Build();

// Usar Serilog para loguear requests HTTP automáticamente
app.UseSerilogRequestLogging();

app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                service = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description,
                duration = e.Value.Duration.ToString()
            }),
            totalDuration = report.TotalDuration
        };
        await System.Text.Json.JsonSerializer.SerializeAsync(context.Response.Body, response);
    }
});

app.Run();
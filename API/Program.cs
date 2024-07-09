using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TiendaContext>(opciones => {
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 2));
    opciones.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion);
} );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using(var alcance = app.Services.CreateScope()){
    var servicios = alcance.ServiceProvider;
    var registradorFac = servicios.GetRequiredService<ILoggerFactory>();  
    try
    {
        var contexto = servicios.GetRequiredService<TiendaContext>(); 
        await contexto.Database.MigrateAsync(); 

    }
    catch (Exception e)
    {
        var registro = registradorFac.CreateLogger<Program>(); 
        registro.LogError(e, "Ocurrio un errorsito durante la migración");
        throw;
    }
}

app.Run();


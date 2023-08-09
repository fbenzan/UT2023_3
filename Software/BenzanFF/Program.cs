using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BenzanFF.Data;
using BenzanFF.Data.Contexts.Interfaces;
using BenzanFF.Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//Se agregar el conexto de la base de datos como contexto. 
builder.Services.AddDbContext<BFFMyDbContext>();
//Se confgura la inyeccion de dependencias del contexto.
builder.Services.AddScoped<IBFFMyDbContext, BFFMyDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
//Actualizar la base de datos segun las migraciones...
using (var scope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BFFMyDbContext>();
    //Se actualiza la base de datos si hay migraciones pendientes
    dbContext.Database.Migrate();
    //Se inicializan los datos, si es requerido...
    await BFFMyDbContextSeeder.Inicializar(dbContext);
}

    app.Run();

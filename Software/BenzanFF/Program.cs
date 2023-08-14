using BenzanFF.Authentication;
using BenzanFF.Data;
using BenzanFF.Data.Contexts;
using BenzanFF.Data.Contexts.Interfaces;
using BenzanFF.Data.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//Se agregar el conexto de la base de datos como contexto. 
builder.Services.AddDbContext<BFFMyDbContext>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

//Se confgura la inyeccion de dependencias del contexto.
builder.Services.AddScoped<IBFFMyDbContext, BFFMyDbContext>();
builder.Services.AddScoped<ICustomAuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();

//Usuario recurrente
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

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
//Con la terminal desde la ubicacion de la solucion, se ejecuta el comando
//dotnet ef migrations add InitialCreate --project BenzanFF --output-dir Data/Contexts/Migrations --context BFFMyDbContext

app.Run();

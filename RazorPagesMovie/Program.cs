using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RazorPagesMovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RazorPagesMovieContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// El siguiente código establece el punto final de excepción /Errory habilita
//el Protocolo de seguridad de transporte estricto (HSTS) HTTP
//cuando la aplicación no se ejecuta en modo de desarrollo:

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Redirige las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();


//Añade coincidencia de rutas a la canalización de middleware. 
app.UseRouting();

//Autoriza a un usuario a acceder a recursos seguros.
app.UseAuthorization();

//Optimice la entrega de recursos estáticos en una aplicación, como HTML, CSS, imágenes y JavaScript
app.MapStaticAssets();

//Configura el enrutamiento de puntos finales para Razor Pages.
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

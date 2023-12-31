using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Vidly.Configurations;
using Vidly.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//add newtonsoft json
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

//add AppDbContext to the services container
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//adding auto mapper
builder.Services.AddAutoMapper(typeof(MapperInitializer));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//define custom routes
// app.MapControllerRoute(
//     name: "MoviesByReleaseDate",
//     pattern: "movies/released/{year}/{month}",
//     defaults: new { controller = "Movies", action = "Random" },
//     constraints: new { year = @"\d{4}", month = @"\d{2}" });


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default", 
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllers();
});

app.Run();
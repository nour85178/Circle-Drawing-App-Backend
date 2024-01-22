using CircleDrawingApp;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


/*var host = new WebHostBuilder()
.ConfigureServices(servicesCollection =>
{
    var serviceProvider = servicesCollection.BuildServiceProvider();
    IConfiguration configuration = (IConfiguration)serviceProvider.GetService(typeof(IConfiguration));
    
})
.UseStartup<Startup>()
.Build();*/


var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer(
    );
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowFrontend");

app.MapControllers();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
   
    endpoints.MapControllerRoute(
        name: "frontend",
        pattern: "{*url}",
        //defaults: new { controller = "CirclesController"});
        defaults: new { controller = "Circles", action = "Index" });
});

app.Run();

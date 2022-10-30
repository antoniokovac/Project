
using Microsoft.EntityFrameworkCore;
using Ninject;
using Project.MVC.AutoMapperProfiles;
using Project.Service;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VehicleDbContext>(options =>
    options.UseSqlServer("Data Source=DESKTOP-N88CHJ5\\SQLEXPRESS;Initial Catalog=VehicleDatabase;Integrated Security=True;"));
builder.Services.AddTransient<IVehicleModelService, VehicleModelService>();
builder.Services.AddTransient<IVehicleMakeService, VehicleMakeService>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=VehicleMake}/{action=Index}/{id?}",
    defaults: new {controller="VehicleMake",action="Index"});

app.Run();

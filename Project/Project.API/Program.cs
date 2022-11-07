using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Repository;
using Project.Repository.Common;
using Project.Service;
using Project.Service.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<VehicleDbContext>(options =>
    options.UseSqlServer("Data Source=DESKTOP-N88CHJ5\\SQLEXPRESS;Initial Catalog=VehicleDatabase;Integrated Security=True;"));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IVehicleMakeRepository, VehicleMakeRepository>();
builder.Services.AddTransient<IVehicleModelRepository, VehicleModelRepository>();
builder.Services.AddTransient<IVehicleMakeService, VehicleMakeService>();
builder.Services.AddTransient<IVehicleModelService, VehicleModelService>();
builder.Services.AddTransient<IGenericRepository, GenericRepository>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();

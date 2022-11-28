using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Project.Common;
using Project.DAL;
using Project.Repository;
using Project.Repository.Common;
using Project.Service;
using Project.Service.Common;
using Project.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
    options.AddPolicy("CORSPolicy",
    builder => {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:3000", "https//appname.azurestaticcapps.net");
    }));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions => 
                                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Vehicle make and model website", Version = "v1"}));

builder.Services.AddDbContext<VehicleDbContext>(options =>
    options.UseSqlServer("Data Source=DINZ;Initial Catalog=VehicleDatabase;Integrated Security=True;"));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IVehicleMakeRepository, VehicleMakeRepository>();
builder.Services.AddTransient<IVehicleModelRepository, VehicleModelRepository>();
builder.Services.AddTransient<IVehicleMakeService, VehicleMakeService>();
builder.Services.AddTransient<IVehicleModelService, VehicleModelService>();
builder.Services.AddTransient<IGenericRepository, GenericRepository>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions => { 
    swaggerUIOptions.DocumentTitle = "Vehicle make and model website";
    swaggerUIOptions.SwaggerEndpoint("swagger/v1/swagger.json", "Vehicle model and make project");
    swaggerUIOptions.RoutePrefix = string.Empty;
    });


app.UseHttpsRedirection();
app.UseCors("CORSPolicy");
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();

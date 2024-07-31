using HotelManagementSystem.Models;
using HotelManagementSystem.DataAccess;
using HotelManagementSystem.DataAccess.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB settings from appsettings.json
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Add MongoDB settings as a singleton
builder.Services.AddSingleton<MongoDBSettings>();

// Register the Services for dependency injection
builder.Services.AddScoped<IRoomService, RoomService>(); 
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();

// Add controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


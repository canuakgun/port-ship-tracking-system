using Microsoft.EntityFrameworkCore;
using PortShipTrackingSystem.Infrastructure.Data;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Infrastructure.Repositories;
using PortShipTrackingSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IShipRepository, ShipRepository>();
builder.Services.AddScoped<IPortRepository, PortRepository>();
builder.Services.AddScoped<IShipVisitRepository, ShipVisitRepository>();
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<ICrewMemberRepository, CrewMemberRepository>();
builder.Services.AddScoped<IShipCrewAssignmentRepository, ShipCrewAssignmentRepository>();
builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<IPortService, PortService>();
builder.Services.AddScoped<IShipVisitService, ShipVisitService>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<ICrewMemberService, CrewMemberService>();
builder.Services.AddScoped<IShipCrewAssignmentService, ShipCrewAssignmentService>();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.MapControllers();
app.Run();


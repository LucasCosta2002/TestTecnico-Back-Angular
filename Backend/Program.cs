using Backend.Models;
using Backend.Models.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add CORS
builder.Services.AddCors(op => op.AddPolicy("AllowWebapp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//add context
builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conexion"));

//automapper
builder.Services.AddAutoMapper(typeof(Program));

//add services
builder.Services.AddScoped<IMascotaRepository, MascotaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using netpostgres;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



string conex = builder.Configuration.GetConnectionString("WebApiDatabase") ?? "";

builder.Services.AddDbContext<PostgresdbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(conex);

}, ServiceLifetime.Singleton, ServiceLifetime.Singleton);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

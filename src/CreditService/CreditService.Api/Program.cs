using CreditService.Application.Service;
using CreditService.Application.Service.Interface;
using CreditService.Application.Mapper;
using CreditService.Domain.Interfaces;
using CreditService.Infrastructure.Data;
using CreditService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register AutoMapper
builder.Services.AddAutoMapper(cfg => { },typeof(MapperClass).Assembly);
// Add services to the container.
builder.Services.AddDbContext<CreditDbContext>(Options =>
{
    Options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});


//Dependency Injection for Services
builder.Services.AddScoped<ICreditAccountService, CreditAccountService>();
builder.Services.AddScoped<ICreditAccount, CreditAccount>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using ApiShared;
using AutoMapper;
using Core;
using Microsoft.AspNetCore.Http.HttpResults;
using PersonsApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAutoMapper(
        typeof(Program),
        typeof(Diware.SL.SystemTextJsonModels.AutoMapper
            .SystemTextJsonModelsProfile))
    .AddDemoCore()
    .AddApiShared();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();

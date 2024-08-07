using EichkustMusic.Statistics.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using static EichkustMusic.Statistics.Infrastructure.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddAutoMapper(typeof(EichkustMusic.Statistics.Infrastructure.MappingProfiles.LikeProfile));

builder.Services.AddApiVersioning(o => 
    o.DefaultApiVersion = new ApiVersion(1, 0));

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

using backend;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
        .AllowAnyHeader());
});

try
{
    builder.Services.AddDbContext<UserDbContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
}
catch (Exception ex)
{
    Console.WriteLine($"Database connection error: {ex.Message}");
    throw new InvalidOperationException("Database connection failed. Please check your configuration.");
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();

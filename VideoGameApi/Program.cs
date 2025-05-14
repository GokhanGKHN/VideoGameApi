using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using VideoGameApi.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// .env dosyasını yükle (önce!)
Env.Load();

// Ortam değişkeninden bağlantı cümlesini al
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

// DbContext'i ekle
builder.Services.AddDbContext<VideoGameDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Dev ortamında Swagger / Scalar API'yi etkinleştir
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

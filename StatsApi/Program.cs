using Microsoft.EntityFrameworkCore;
using StatsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // your React dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<StatsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// app.UseHttpsRedirection();

app.UseCors("AllowReactApp");
app.UseAuthorization();

app.MapControllers();

app.Run();

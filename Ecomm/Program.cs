using Ecomm.Data;
using Ecomm.Models;
using Ecomm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<AdressService>();
builder.Services.AddScoped<UserService>();

// builder.Services.Configure<ApiBehaviorOptions>(options =>
// {
//     options.SuppressModelStateInvalidFilter = false;
// });
builder.Services.AddDbContext<DatabaseConnection>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

var app = builder.Build();


// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//
//     SeedData.Initialize(services);
// }
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.MapControllers();


app.Run();


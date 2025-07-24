using Ecomm.Data;
using Ecomm.Exceptions;
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

/*Services qe lidhen me Backend kta e kom bo qe me mundesu Versionimin ma te lehte*/
builder.Services.AddScoped<AdressService>();
builder.Services.AddScoped<UserService>();



/*Qekjo pjes osht qe me pas te standartizum resposne qe bahet nese ne DTO eshte derguar gabim, kta e kom bo qe ne Frontend me u lehtu
 https://stackoverflow.com/questions/65698991/how-to-avoid-invalidmodelstateresponsefactory-to-interfere-with-an-error-respons
 Ku e kom marr kodin
 */
builder.Services.Configure<ApiBehaviorOptions>(options 
    => options.InvalidModelStateResponseFactory = 
        (context) =>
        {
            var error = context!.ModelState.FirstOrDefault().Value!.Errors.FirstOrDefault()!.ErrorMessage;
            var result = new BadRequestResponse(){success = false,errorMessage = error};
            return new JsonResult(result);
        });

builder.Services.AddDbContext<DatabaseConnection>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.MapControllers();


app.Run();


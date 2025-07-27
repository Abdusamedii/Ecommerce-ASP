using System.Text;
using CalConnect.Api.Users.Infrastructure;
using dotenv.net;
using Ecomm.Data;
using Ecomm.Exceptions;
using Ecomm.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
/*Kod qe e kom gjet te ni blog post qe me ja mundesu swaggerit fast Bearer token*/
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Abdusamed's Ecommerce", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddControllers();


/*Services qe lidhen me Backend kta e kom bo qe me mundesu Versionimin ma te lehte*/
builder.Services.AddScoped<AdressService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();

/*JWT AUTHENTICATION*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!)),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthentication();


/*Qekjo pjes osht qe me pas te standartizum resposne qe bahet nese ne DTO eshte derguar gabim, kta e kom bo qe ne Frontend me u lehtu
 https://stackoverflow.com/questions/65698991/how-to-avoid-invalidmodelstateresponsefactory-to-interfere-with-an-error-respons
 Ku e kom marr kodin
 */
builder.Services.Configure<ApiBehaviorOptions>(options
    => options.InvalidModelStateResponseFactory =
        context =>
        {
            var error = context!.ModelState.FirstOrDefault().Value!.Errors.FirstOrDefault()!.ErrorMessage;
            var result = new BadRequestResponse { success = false, errorMessage = error };
            return new JsonResult(result);
        });

builder.Services.AddDbContext<DatabaseConnection>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
                     throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.MapControllers().RequireAuthorization();

app.Run();
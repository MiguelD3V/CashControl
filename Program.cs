using Cashcontrol.API.Banco;
using Cashcontrol.API.Banco.Interfaces;
using Cashcontrol.API.Banco.Repositories;
using Cashcontrol.API.Controllers;
using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Data.Repositories;
using Cashcontrol.API.Helpers;
using Cashcontrol.API.Helpers.Interface;
using Cashcontrol.API.Mapping;
using Cashcontrol.API.Services.Validators;
using Cashcontrol.API.Services.Validators.Interfaces;
using Cashcontrol.API.Services.Workers;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging());
    
// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

//Expense dependencies
#region
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ExpenseMapper>());
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseValidator, ExpenseValidator>();
#endregion

//Account dependecies
#region
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AccountMapper>());
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountValidator, AccountValidator>();
#endregion

//Income dependencies
#region
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<IncomeMapper>());
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IIncomeValidator, IncomeValidator>();
#endregion

//User dependencies
#region
builder.Services.AddScoped<IUserService, AuthenticationService>();
builder.Services.AddScoped<IUserValidator, AuthenticationValidator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<IJwtTokenManager, JwtTokenManager>();
#endregion


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["JwtSettings:SecurityKey"]
                    ?? throw new InvalidOperationException("JwtSettings:SecurityKey não está configurado.")
                )
            ),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Digite: Bearer {seu token JWT}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new
            {
                field = e.Key,
                description = e.Value.Errors.First().ErrorMessage
            }).ToList();

        var response = new
        {
            status = 400,
            message = "Não foi possível processar sua solicitação. Corrija os campos abaixo:",
            errors,
            traceId = context.HttpContext.TraceIdentifier
        };

        return new BadRequestObjectResult(response);
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

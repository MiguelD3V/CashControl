using Cashcontrol.API.Banco;
using Cashcontrol.API.Banco.Interfaces;
using Cashcontrol.API.Banco.Repositories;
using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Data.Repositories;
using Cashcontrol.API.Mapping;
using Cashcontrol.API.Services.Validators;
using Cashcontrol.API.Services.Validators.Interfaces;
using Cashcontrol.API.Services.Workers;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<IncomeMapper>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AccountMapper>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ExpenseMapper>());
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountValidator, AccountValidator>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IIncomeValidator, IncomeValidator>();
builder.Services.AddScoped<IExpenseValidator, ExpenseValidator>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Products.API.Bus;
using Products.API.Data;
using Products.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbcontext>(opt =>
{
    opt.UseSqlite("Data Source=app.db");
});

builder.Services.AddScoped<IBusService, MassTransitBusService>();
builder.Services.AddMassTransit(c =>
{
    c.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
    });
});

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

using Inventory.API.Bus;
using Inventory.API.Data;
using Inventory.API.Data.Repository;
using Inventory.API.EventHandlers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Library.Events.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbcontext>(opt =>
{
    opt.UseSqlite("Data Source=app.db");
});

builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();

builder.Services.AddScoped<IBusService, MassTransitBusService>();
builder.Services.AddMassTransit(c =>
{
    c.AddConsumer<ProductCreatedEventHandler>();
    c.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbitmq://messagebroker", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

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

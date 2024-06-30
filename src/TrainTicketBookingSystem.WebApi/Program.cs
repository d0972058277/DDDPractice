using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TrainTicketBookingSystem.Application;
using TrainTicketBookingSystem.Infrastructure;
using Mediator = TrainTicketBookingSystem.Infrastructure.Architecture.Mediator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("TrainTicketBookingSystem.Application"));
});
builder.Services.TryAddScoped<TrainTicketBookingSystem.Application.Architecture.IMediator, Mediator>();
builder.Services.TryAddScoped<ITrainRepository, TrainRepository>();
builder.Services.TryAddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddDbContext<TrainTicketBookingSystemDbContext>(opt =>
{
    const string connectionString =
        "Server=localhost; Port=3306; User ID=root; Password=root; Database=TrainTicketBookingSystem;";
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

public partial class Program
{
}
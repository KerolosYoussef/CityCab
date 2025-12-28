using CityCab.Rider.API;
using CityCab.Rider.API.Features.RiderManagements.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCarter();
builder.Services.AddRiderAPIServices();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    // TODO cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


builder.AddNpgsqlDbContext<ApplicationDbContext>("rider-db");

var app = builder.Build();

await app.Services.MigrateData();

app.MapCarter();

app.UseExceptionHandler();

app.Run();

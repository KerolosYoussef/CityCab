using CityCab.Rider.API.Infrastructure.Hubs;
using CityCab.Trip.API;

var builder = WebApplication.CreateBuilder(args);
Assembly currentAssembly = typeof(Program).Assembly;

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCarter();
builder.Services.AddSignalR();
builder.Services.AddRiderAPIServices();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(currentAssembly);

builder.Services.AddGrpcClient<FareEstimateService.FareEstimateServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:TripUrl"]!);
});

builder.Services.AddMessageBrokerWithOutbox<ApplicationDbContext>
    (builder.Configuration, currentAssembly);

builder.AddNpgsqlDbContext<ApplicationDbContext>("rider-db");

var app = builder.Build();

await app.Services.MigrateData();

app.MapCarter();

app.UseExceptionHandler();

app.MapHub<RideHub>("/hubs/ride");

await app.RunAsync();

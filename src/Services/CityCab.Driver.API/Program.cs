var builder = WebApplication.CreateBuilder(args);

Assembly currentAssembly = typeof(Program).Assembly;

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCarter();
builder.Services.AddSignalR();
builder.Services.AddDriverAPIServices();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(currentAssembly);

builder.Services.AddMessageBrokerWithOutbox<ApplicationDbContext>
    (builder.Configuration, currentAssembly);

builder.AddNpgsqlDbContext<ApplicationDbContext>("driver-db");

var app = builder.Build();

await app.Services.MigrateData();

app.MapCarter();

app.UseExceptionHandler();

await app.RunAsync();
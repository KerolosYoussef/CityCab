var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithHostPort(5433)
    .WithDataVolume()
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var driversDatabase = postgres.AddDatabase("drivers-db");
var tripDatabase = postgres.AddDatabase("citycab-trip");
var riderDatabase = postgres.AddDatabase("rider-db");

var redis = builder.AddRedis("redis")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var rmq = builder.AddRabbitMQ("messaging")
                 .WithManagementPlugin();


builder.AddProject<Projects.CityCab_Trip_API>("citycab-trip-api")
    .WithReference(tripDatabase)
    .WaitFor(tripDatabase)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(rmq)
    .WaitFor(rmq);

builder.AddProject<Projects.CityCab_Driver_API>("citycab-driver-api")
    .WithReference(driversDatabase)
    .WaitFor(driversDatabase)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(rmq)
    .WaitFor(rmq);

builder.AddProject<Projects.CityCab_Rider_API>("citycab-rider-api")
    .WithReference(riderDatabase)
    .WaitFor(riderDatabase)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(rmq)
    .WaitFor(rmq);

builder.Build().Run();

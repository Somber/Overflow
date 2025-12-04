var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloak("Keycloak", 6001)
    .WithDataVolume("keycloak-data");

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.WattReise_ApiService>("apiservice");

builder.AddProject<Projects.WattReise_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();

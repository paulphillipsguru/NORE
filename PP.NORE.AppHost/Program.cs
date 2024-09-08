var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PP_NORE_API>("pp-nore-api");

builder.Build().Run();

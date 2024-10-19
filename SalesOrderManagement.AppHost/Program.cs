var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SalesOrderManagement_Client>("SalesOrderManagement-Client");

builder.AddProject<Projects.SalesOrderManagement_API>("SalesOrderManagement-API");

await builder.Build().RunAsync();
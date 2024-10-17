using SalesOrderManagement.API.Endpoints.SalesOrder;
using SalesOrderManagement.Application.Extensions;
using SalesOrderManagement.Infrastructure.Extensions;
using SalesOrderManagement.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Register common service defaults
builder.AddServiceDefaults();

// Get the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register services for each layer using extensions
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(connectionString);
builder.Services.AddAuthorization();
// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();
// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Use HTTPS redirection and authorization middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Map minimal API endpoints
app.MapSalesOrderEndpoints(); // Register the SalesOrder minimal APIs

// Run the application
app.Run();
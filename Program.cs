using API.Extensions;

var builder = WebApplication.CreateBuilder(args); // Initializing a builder to configure the web application
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
var app = builder.Build(); // Building the web application

// Configure the HTTP request pipeline (middleware configuration)
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger(); // Enable Swagger for API documentation in development mode
//     app.UseSwaggerUI(); // Enable Swagger UI for testing API endpoints in development mode
// }

// Middleware setup to handle incoming HTTP requests

// app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS for secure communication (currently commented out)

// app.UseAuthorization(); // Enable authorization middleware (currently commented out)

// Configuring CORS to allow requests from specific origins (e.g., Angular front-end running on localhost)
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();
// Map controller endpoints, routing incoming requests to the corresponding controllers
app.MapControllers();

// Start the application
app.Run();

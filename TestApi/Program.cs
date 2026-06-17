using System.Diagnostics;
using TestApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Run API on localhost:8080
builder.WebHost.UseUrls("http://localhost:8080");

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<ICandidateService, CandidateService>();

// Allow all CORS requests (Development/Assignment only)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
    c.RoutePrefix = string.Empty;
});

// IMPORTANT: DO NOT USE HTTPS REDIRECTION
// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Open Swagger automatically
if (app.Environment.IsDevelopment())
{
    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "http://localhost:8080",
            UseShellExecute = true
        });
    }
    catch
    {
        // Ignore browser launch failures
    }
}

app.Run();
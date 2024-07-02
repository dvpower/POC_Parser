using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
        { 
            Title = "Healthcare Claims 837i Parser API v1", 
            Version = "v1",
            Description = "An API (Proof Of Concept) for parsing Electronic Data Interchange (EDI) files for an 837I (Institutional) healthcare claim.",
            Contact = new OpenApiContact
            {
                Name = "API Support",
                Email = "support@grahammurphy.com"
            },
            Extensions =
                    {
                        ["x-developer"] = new OpenApiObject
                        {
                            ["name"] = new OpenApiString("David Power"),
                            ["email"] = new OpenApiString("mrdavidpower@gmail.com"),
                        }
                    }
    });

    // Set the comments path for the Swagger JSON and UI.
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

// Add logging
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Custom error handling middleware
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception, "An unhandled exception occurred.");

            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An error occurred.",
                DetailedMessage = exception?.Message,
                StackTrace = exception?.StackTrace
            });
        });
    });
}

// Enable Swagger for all environments
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Healthcare Claims 837i Parser API v1"));


app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Add this line to redirect root to Swagger UI
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using ProjectManagement.Application;
using ProjectManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(opt => 
{
    opt.DocumentSettings = s =>
    {
        s.Title = "ProjectManagement Service";
        s.Version = "v1.0";
    };
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthenticationJwtBearer(e => e.SigningKey = "Secrets");
builder.Services.AddAuthorization();
builder.Services.AddCors(opt => {
    opt.AddDefaultPolicy(policy => {
        policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
    app.UseSwaggerGen();

app.Run();

public partial class Program;

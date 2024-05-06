using AuthContext.Infrastructure;
using AuthContext.Application;
using FastEndpoints;
using FastEndpoints.Swagger;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(opt => 
{
    opt.DocumentSettings = s =>
    {
        s.Title = "Auth Service";
        s.Version = "v1.0";
    };
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, typeof(DataProtectorTokenProvider<>));
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

using IntegrationContext.Infrastructure;
using IntegrationContext.Application;
using FastEndpoints.Security;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(opt => 
{
    opt.DocumentSettings = s =>
    {
        s.Title = "Integration Service";
        s.Version = "v1.0";
    };
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthenticationJwtBearer(e => e.SigningKey = builder.Configuration["JwtOptions:SecretKey"]);
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

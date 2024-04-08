using FastEndpoints;
using FastEndpoints.Swagger;
using ProjectManagement.Application;

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

var app = builder.Build();

app.UseHttpsRedirection();
app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
    app.UseSwaggerGen();

app.Run();

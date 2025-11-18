using System.Reflection;
using VoitingApp.Domain;
using VoitingApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();   
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();           

builder.Services.AddSingleton<IPolesRepository, PolesRepository>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<CreatePoleDto, PoleDto>();
}, Array.Empty<Assembly>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    
    app.UseSwaggerUI();
}

app.MapControllers();                

app.Run();


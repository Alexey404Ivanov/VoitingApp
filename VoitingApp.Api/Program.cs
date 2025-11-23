using System.Reflection;
using VoitingApp.Domain;
using VoitingApp.Infrastructure;
using VoitingApp.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});


builder.Services.AddControllers();   

builder.Services.AddEndpointsApiExplorer(); 

builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<IPolesRepository, PolesRepository>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<PoleEntity, PoleDto>();
    cfg.CreateMap<CreatePoleDto, PoleEntity>();
    cfg.CreateMap<CreateOptionDto, OptionEntity>();
    cfg.CreateMap<OptionEntity, OptionDto>();
    cfg.CreateMap<OptionEntity, OptionResultsDto>();
    cfg.CreateMap<PoleEntity, PoleResultsDto>();
}, Array.Empty<Assembly>());


var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/poles"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    
    app.UseSwaggerUI();
}

app.UseRouting();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Incoming: {context.Request.Method} {context.Request.Path}");
    await next();
    Console.WriteLine($"Outgoing: {context.Response.StatusCode}");
});


app.MapControllers();                

app.UseStaticFiles();

app.MapRazorPages();

app.Run();


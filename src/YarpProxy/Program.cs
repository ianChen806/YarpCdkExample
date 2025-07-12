using YarpProxy.Interfaces;
using YarpProxy.Providers;
using YarpProxy.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IDomainHeaderService, DomainHeaderService>();

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms<OriginHeaderTransformProvider>();

var app = builder.Build();
app.MapReverseProxy();
app.Run();

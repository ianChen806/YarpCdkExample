using Amazon.S3;
using YarpProxy.Configs;
using YarpProxy.Interfaces;
using YarpProxy.Providers;
using YarpProxy.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddScoped<IDomainHeaderService, DomainHeaderService>();
builder.Services.AddScoped<IAmazonS3, AmazonS3Client>();
builder.Services.Configure<S3Config>(configuration.GetSection("S3Config"));

builder.Services.AddMemoryCache();
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(configuration.GetSection("ReverseProxy"))
    .AddTransforms<OriginHeaderTransformProvider>();

var app = builder.Build();
app.MapReverseProxy();
app.Run();

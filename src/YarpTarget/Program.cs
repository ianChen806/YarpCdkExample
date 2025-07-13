var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/source", (IHttpContextAccessor contextAccessor) =>
    {
        contextAccessor.HttpContext!.Request.Headers.TryGetValue("X-Source", out var source);
        return source.FirstOrDefault() ?? "empty";
    })
    .WithName("Get source from header")
    .WithOpenApi();

app.MapGet("/", () => "ok");
app.Run();

using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;
using YarpProxy.Interfaces;

namespace YarpProxy.Providers;

public class OriginHeaderTransformProvider : ITransformProvider
{
    private readonly IDomainHeaderService _svc;
    private Dictionary<string, string> _map = new();

    public OriginHeaderTransformProvider(IDomainHeaderService svc)
    {
        _svc = svc;
        _ = RefreshLoopAsync();
    }

    public void ValidateRoute(TransformRouteValidationContext context)
    {
    }

    public void ValidateCluster(TransformClusterValidationContext context)
    {
    }

    public void Apply(TransformBuilderContext context)
    {
        context.AddRequestTransform(async r =>
        {
            var origin = GetOrigin(r);
            if (origin != null && _map.TryGetValue(origin, out var header))
            {
                r.ProxyRequest.Headers.Add("X-Source", header);
            }
            else
            {
                r.ProxyRequest.Headers.Add("X-Source", "unknow");
            }

            await Task.CompletedTask;
        });
    }

    private string? GetOrigin(RequestTransformContext r)
    {
        r.HttpContext.Request.Headers.TryGetValue("Source", out var source);
        return source;
    }

    private async Task RefreshLoopAsync()
    {
        while (true)
        {
            _map = await _svc.GetAllAsync();
            await Task.Delay(TimeSpan.FromMinutes(5));
        }
    }
}

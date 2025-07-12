using YarpProxy.Interfaces;

namespace YarpProxy.Services;

public class DomainHeaderService : IDomainHeaderService
{
    public Task<Dictionary<string, string>> GetAllAsync()
    {
        return Task.FromResult(new Dictionary<string, string>()
        {
            { "a.com", "A" },
            { "b.com", "B" },
            { "c.com", "C" },
        });
    }
}
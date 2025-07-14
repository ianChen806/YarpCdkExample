using YarpProxy.Domain.Models;

namespace YarpProxy.Interfaces;

public interface IDomainHeaderService
{
    Task<Dictionary<string, DomainSetting>> GetAllAsync();
}

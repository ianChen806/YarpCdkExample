namespace YarpProxy.Interfaces;

public interface IDomainHeaderService
{
    Task<Dictionary<string, string>> GetAllAsync();

}
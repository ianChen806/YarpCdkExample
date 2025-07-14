using System.Text.Json;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using YarpProxy.Configs;
using YarpProxy.Domain.Models;
using YarpProxy.Interfaces;

namespace YarpProxy.Services;

public class DomainHeaderService : IDomainHeaderService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IAmazonS3 _s3Client;
    private readonly ILogger<DomainHeaderService> _logger;
    private readonly S3Config _config;

    public DomainHeaderService(
        IOptions<S3Config> config,
        IMemoryCache memoryCache,
        IAmazonS3 s3Client,
        ILogger<DomainHeaderService> logger)
    {
        _memoryCache = memoryCache;
        _s3Client = s3Client;
        _logger = logger;
        _config = config.Value;
    }

    public async Task<Dictionary<string, DomainSetting>> GetAllAsync()
    {
        const string cacheKey = "DomainMappings";

        if (_memoryCache.TryGetValue(cacheKey, out Dictionary<string, DomainSetting>? cachedMappings))
        {
            return cachedMappings!;
        }

        var mappings = await LoadFromS3();
        _memoryCache.Set(cacheKey, mappings, TimeSpan.FromMinutes(_config.CacheExpiryMinutes));

        return mappings;
    }

    private async Task<Dictionary<string, DomainSetting>> LoadFromS3()
    {
        try
        {
            using var response = await _s3Client.GetObjectAsync(new GetObjectRequest()
            {
                BucketName = _config.BucketName,
                Key = _config.ConfigFileName
            });
            using var reader = new StreamReader(response.ResponseStream);
            var jsonContent = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<Dictionary<string, DomainSetting>>(jsonContent)!;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error loading domain mappings from S3");
        }
        return new Dictionary<string, DomainSetting>();
    }
}

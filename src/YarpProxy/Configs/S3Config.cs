namespace YarpProxy.Configs;

public class S3Config
{
    public string BucketName { get; set; } = string.Empty;

    public string ConfigFileName { get; set; } = string.Empty;

    public int CacheExpiryMinutes { get; set; } = 5;
}

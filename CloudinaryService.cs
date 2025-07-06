using CloudinaryDotNet;
using System.Security.Cryptography;
using System.Text;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;
    private readonly string _apiSecret;

    public CloudinaryService(CloudinaryConfig config)
    {
        _apiSecret = config.ApiSecret;
        _cloudinary = new Cloudinary(new Account(config.CloudName, config.ApiKey, config.ApiSecret));
    }

    public (string signature, string timestamp, string apiKey, string cloudName) GenerateUploadSignature(string? folder = null)
    {
        var timestamp = ((int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()).ToString();
        var parameters = new SortedDictionary<string, object>
        {
            { "timestamp", timestamp }
        };
        
        // Add folder parameter if provided
        if (!string.IsNullOrEmpty(folder))
        {
            parameters.Add("folder", folder);
        }
        
        var signature = _cloudinary.Api.SignParameters(parameters);
        return (signature, timestamp, _cloudinary.Api.Account.ApiKey, _cloudinary.Api.Account.Cloud);
    }
} 
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using StoronnimV.Application.Interfaces.BlobAzure;

namespace StoronnimV.Application.Services.BlobAzure;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobService(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("BlobStorageConnectionString");
        _blobServiceClient = new BlobServiceClient(connectionString);
    }
    
    public async Task<string> AddFileAsync(string containerName, string fileName, Stream fileStream, CancellationToken ct)
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync(cancellationToken: ct);
        
        var blobClient = container.GetBlobClient(fileName);
        
        await blobClient.UploadAsync(fileStream, overwrite: true, cancellationToken: ct);

        return blobClient.Uri.ToString();
    }

    public string GetFileUrl(string containerName, string fileName, CancellationToken ct)
    {
        var blobClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blob = blobClient.GetBlobClient(fileName);
        
        return blob.Uri.ToString();
    }

    public async Task DeleteFileAsync(string containerName, string fileName, CancellationToken ct)
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        
        var blobClient = container.GetBlobClient(fileName);
        
        await blobClient.DeleteIfExistsAsync(cancellationToken: ct);
    }
}
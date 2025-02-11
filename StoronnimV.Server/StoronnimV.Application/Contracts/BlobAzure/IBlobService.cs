namespace StoronnimV.Application.Contracts.BlobAzure;

public interface IBlobService
{
    Task<string> AddFileAsync(string containerName, string fileName, Stream fileStream, CancellationToken ct);
    string GetFileUrl(string containerName, string fileName, CancellationToken ct);
    Task DeleteFileAsync(string containerName, string fileName, CancellationToken ct);
}
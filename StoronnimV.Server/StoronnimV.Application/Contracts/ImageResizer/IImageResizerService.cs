using Microsoft.AspNetCore.Http;

namespace StoronnimV.Application.Contracts.ImageResizer;

public interface IImageResizerService
{
    Task<byte[]> ResizeImageIfNecessaryAsync(IFormFile photo, int width, int height, CancellationToken ct);
    Task<byte[]> ResizeImageMaxIfNecessaryAsync(IFormFile photo, int width, int height, CancellationToken ct);
}
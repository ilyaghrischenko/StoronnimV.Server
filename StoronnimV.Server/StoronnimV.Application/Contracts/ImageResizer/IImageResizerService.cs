using Microsoft.AspNetCore.Http;

namespace StoronnimV.Application.Contracts.ImageResizer;

public interface IImageResizerService
{
    Task<byte[]> ResizeImageByCompressIfNecessaryAsync(IFormFile photo, int width, int height, CancellationToken ct);
    Task<byte[]> ResizeImageByCroppingIfNecessaryAsync(IFormFile photo, int width, int height, CancellationToken ct);
}
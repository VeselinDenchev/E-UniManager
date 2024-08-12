using CloudinaryDotNet.Actions;

using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Cloudinary.Interfaces;

public interface ICloudinaryService
{
    Task<CloudinaryFile> UploadAsync(byte[] fileBytes, string mimeType, CancellationToken cancellationToken);

    Task<CloudinaryFile> UpdateAsync(string id, byte[] fileBytes, string mimeType, CancellationToken cancellationToken);

    Task DeleteAsync(CloudinaryFile cloudinaryFile);

    Task<(byte[] fileBytes, string mimeType)> DownloadAsync(string id, CancellationToken cancellationToken);

    Task<PingResult> PingAsync(CancellationToken cancellationToken);
}
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Cloudinary.Interfaces;

public interface ICloudinaryService
{
    Task<CloudinaryFile> UploadAsync(byte[] fileBytes, string mimeType, CancellationToken cancellationToken);

    Task<CloudinaryFile> UpdateAsync(string id, byte[] fileBytes, string mimeType, CancellationToken cancellationToken);

    Task DeleteByIdAsync(string id);

    Task DeleteByIdAndExtensionAsync(string id, string fileExtension);

    Task<(byte[] fileBytes, string mimeType)> DownloadAsync(string id, CancellationToken cancellationToken);
}
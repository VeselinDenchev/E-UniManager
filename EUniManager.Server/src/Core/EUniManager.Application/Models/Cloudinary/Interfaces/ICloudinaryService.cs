namespace EUniManager.Application.Models.Cloudinary.Interfaces;

public interface ICloudinaryService
{
    Task<string> UploadAsync(byte[] fileBytes, string mimeType, CancellationToken cancellationToken);
    
    Task UpdateAsync(string publicId, byte[] newFileBytes, string newMimeType, CancellationToken cancellationToken);

    Task<(byte[] fileBytes, string mimeType)> DownloadAsync(string publicId, CancellationToken cancellationToken);
}
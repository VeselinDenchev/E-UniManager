namespace EUniManager.Application.Models.Cloudinary.Dtos;

public sealed record UploadFileDto
{
    public byte[] Bytes { get; set; } = null!;

    public string MimeType { get; set; } = null!;
}
using CloudinaryDotNet.Actions;

using EUniManager.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class CloudinaryFileMapper
{
    // OriginalFileName = PublicId without the file extension on raw uploads
    [MapProperty(nameof(RawUploadResult.OriginalFilename), nameof(CloudinaryFile.Id))]
    public partial CloudinaryFile Map(RawUploadResult entity);
}
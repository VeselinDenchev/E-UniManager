using EUniManager.Application.Models.Cloudinary.Dtos;
using EUniManager.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class FileMapper
{
    public partial FileDto Map(CloudinaryFile cloudinaryFile);
}
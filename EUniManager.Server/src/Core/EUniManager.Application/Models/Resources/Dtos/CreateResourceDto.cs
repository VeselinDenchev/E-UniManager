using EUniManager.Application.Models.Assignments.Dtos;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Cloudinary.Dtos;

namespace EUniManager.Application.Models.Resources.Dtos;

public sealed record CreateResourceDto : ICreateDto
{
    public Guid ActivityId { get; set; }
    
    public string Title { get; set; } = null!;
    
    public string ResourceType { get; set; } = null!;
    
    public string? Info { get; set; }

    public UploadFileDto? File { get; set; } = null!;
}
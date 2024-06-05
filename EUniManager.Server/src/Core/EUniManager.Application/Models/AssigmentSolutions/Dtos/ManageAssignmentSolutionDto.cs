using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Cloudinary.Dtos;

namespace EUniManager.Application.Models.AssigmentSolutions.Dtos;

public sealed record ManageAssignmentSolutionDto : ICreateDto, IUpdateDto
{
    public Guid AssignmentId { get; set; }

    public UploadFileDto? File { get; set; }
    
    public string? Text { get; set; }
}
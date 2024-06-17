using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Enums;

namespace EUniManager.Application.Models.AssigmentSolutions.Dtos;

public sealed record AssignmentSolutionDetailsDto : IDetailsDto
{
    public Guid Id { get; set; }
    
    public string? FileId { get; set; }

    public string? FileExtension { get; set; }

    public string? CreatedAt { get; set; }

    public string? SeenOn { get; set; }
    
    public string? UploadedOn { get; set; }
    
    public Mark? Mark { get; set; }

    public string? MarkedOn { get; set; }

    public string? Comment { get; set; }
}
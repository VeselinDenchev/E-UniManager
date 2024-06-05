using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Enums;

namespace EUniManager.Application.Models.AssigmentSolutions.Dtos;

public sealed record AssignmentSolutionDetailsDto : IDetailsDto
{
    public string? FileId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? SeenOn { get; set; }
    
    public DateTime? UploadedOn { get; set; }
    
    public Mark? Mark { get; set; }

    public DateTime? MarkedOn { get; set; }

    public string? Comment { get; set; }
}
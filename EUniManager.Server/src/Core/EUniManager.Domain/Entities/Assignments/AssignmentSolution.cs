﻿using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities.Assignments;

public class AssignmentSolution : BaseEntity<Guid>
{
    public Assignment Assignment { get; set; } = null!;
    
    public Student Student { get; set; } = null!;
    
    public CloudinaryFile? File { get; set; }

    public DateTime? SeenOn { get; set; }
    
    public DateTime? UploadedOn { get; set; }
    
    public Mark? Mark { get; set; }

    public DateTime? MarkedOn { get; set; }

    public string? Comment { get; set; }
}
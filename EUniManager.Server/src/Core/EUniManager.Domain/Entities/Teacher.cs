﻿using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Assignments;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Domain.Entities;

public class Teacher : BaseEntity<Guid>
{
    public IdentityUser<Guid> User { get; set; } = null!;
    
    public string? Rank { get; set; }
    
    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;

    public List<Subject> LecturingSubjects { get; set; } = null!;
    
    public List<Subject> AssistingSubjects { get; set; } = null!;

    public List<Assignment> Assignments { get; set; } = null!;
}
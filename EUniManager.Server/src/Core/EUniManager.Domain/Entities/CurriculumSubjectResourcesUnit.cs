﻿using EUniManager.Domain.Abstraction.Base;

namespace EUniManager.Domain.Entities;

public class CurriculumSubjectResourcesUnit : BaseEntity<Guid>
{
    public byte Semester { get; set; }

    public Subject Subject { get; set; } = null!;

    public List<Resource> Resources { get; set; } = null!;
}
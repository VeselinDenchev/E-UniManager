﻿using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Teachers.Dtos;

public record CreateTeacherDto : ICreateDto
{
    public string? Rank { get; init; }
    
    public string FirstName { get; init; } = null!;

    public string MiddleName { get; init; } = null!;
    
    public string LastName { get; init; } = null!;
}
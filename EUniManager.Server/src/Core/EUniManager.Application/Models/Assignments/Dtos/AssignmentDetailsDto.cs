﻿using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Assignments.Dtos;

public record AssignmentDetailsDto : IDetailsDto
{
    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string StartDate { get; set; } = null!;

    public string DueDate { get; set; } = null!;
    
    public string? Description { get; set; }
}
﻿using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.IndividualProtocols.Dtos;

public sealed record IndividualProtocolDetailsDto : IDetailsDto
{
    public required string SubjectCourseName { get; set; } = null!;

    public string Status { get; set; } = null!;
}
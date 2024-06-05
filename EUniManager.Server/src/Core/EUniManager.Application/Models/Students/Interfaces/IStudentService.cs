﻿using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Students.Dtos;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Application.Models.Students.Interfaces;

public interface IStudentService : IBaseService<Student, Guid, StudentDto, StudentDetailsDto>
{
    Task<StudentHeaderDto> GetHeaderDataAsync(Guid id, CancellationToken cancellationToken);

    Task CertifySemesterAsync(Guid id, byte semester, CancellationToken cancellationToken);

    Task UpdateSpecialtyAsync(Guid studentId, Guid specialtyId, CancellationToken cancellationToken);

    Task UpdateStatusAsync(Guid id, StudentStatus status, CancellationToken cancellationToken);

    Task UpdateGroupNumberAsync(Guid id, byte groupNumber, CancellationToken cancellationToken);

    Task UpdateEnrolledInSemesterAsync(Guid id, byte semester, CancellationToken cancellationToken);

    Task UpdateEmailAsync(Guid id, string email, CancellationToken cancellationToken);
}
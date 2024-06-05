using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Subjects.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

namespace EUniManager.Application.Models.Subjects.Interfaces;

public interface ISubjectService : IBaseService<Subject, Guid, SubjectDto, SubjectDetailsDto>
{
    Task<List<StudentSubjectInfoDto>> GetStudentSubjectsInfoAsync(CancellationToken cancellationToken);
    
    Task UpdateMarkAsync(Guid subjectId, Guid studentId, Mark mark, CancellationToken cancellationToken);
}
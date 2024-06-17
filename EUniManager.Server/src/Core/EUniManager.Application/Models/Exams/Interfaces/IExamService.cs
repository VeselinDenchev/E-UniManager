using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Exams.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Exams.Interfaces;

public interface IExamService : IBaseService<Exam, Guid, ExamDto, ExamDetailsDto>
{
    Task<List<StudentExamDto>> GetAllForStudentAsync(CancellationToken cancellationToken);
}
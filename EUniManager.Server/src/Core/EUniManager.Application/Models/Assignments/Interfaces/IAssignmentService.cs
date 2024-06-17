using EUniManager.Application.Models.Assignments.Dtos;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Assignments.Interfaces;

public interface IAssignmentService : IBaseService<Assignment, Guid, IEntityDto, AssignmentDetailsDto>
{
    ValueTask<AssignmentWithSolutionDto> GetByIdWithStudentSolutionAsync(Guid id, CancellationToken cancellationToken);
    
    Task<List<AssignmentDto>> GetStudentAssignmentsAsync(CancellationToken cancellationToken);
    
    Task<List<AssignmentDto>> GetTeacherAssignmentsAsync(CancellationToken cancellationToken);

    Task DeleteByAssignmentAsync(Assignment assignment);
}
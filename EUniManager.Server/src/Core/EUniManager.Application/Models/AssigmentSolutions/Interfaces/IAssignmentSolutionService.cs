using EUniManager.Application.Models.AssigmentSolutions.Dtos;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Application.Models.AssigmentSolutions.Interfaces;

public interface IAssignmentSolutionService :
    IBaseService<AssignmentSolution, Guid, IEntityDto, AssignmentSolutionDetailsDto>
{
    Task CreateAsync(Assignment assignment, Student student, CancellationToken cancellationToken);
    Task<List<AssignmentSolutionDto>> GetAllSolutionsToAssignmentAsync(Guid assignmentId, CancellationToken cancellationToken);
    Task UpdateMarkAsync(Guid id, Mark mark, CancellationToken cancellationToken);
    Task UpdateCommentAsync(Guid id, string comment, CancellationToken cancellationToken);
    Task DeleteAllSolutionsToAssignmentAsync(List<AssignmentSolution> assignmentSolutions, AssignmentType assignmentType);
}
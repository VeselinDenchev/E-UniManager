using EUniManager.Application.Models.AssigmentSolutions.Dtos;
using EUniManager.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class AssignmentSolutionMapper
{
    [UserMapping]
    public List<AssignmentSolutionDto> Map(List<AssignmentSolution> entities)
    {
        return entities.Select(asol => new AssignmentSolutionDto
        {
            Id = asol.Id,
            StudentFacultyNumber = asol.Student.ServiceData.FacultyNumber,
            StudentFullName = asol.Student.FullName,
            FileId = asol.File?.Id,
            Text = asol.Text,
            Mark = asol.Mark
        }).ToList();
    }
    
    [MapProperty(nameof(@AssignmentSolution.File.Id), nameof(AssignmentSolutionDetailsDto.FileId))]
    public partial AssignmentSolutionDetailsDto Map(AssignmentSolution entity);
    
    [MapperIgnoreTarget(nameof(AssignmentSolution.Id))]
    [MapperIgnoreTarget(nameof(AssignmentSolution.CreatedAt))]
    [MapperIgnoreTarget(nameof(AssignmentSolution.ModifiedAt))]
    [MapperIgnoreTarget(nameof(AssignmentSolution.Assignment))]
    [MapperIgnoreTarget(nameof(AssignmentSolution.Student))]
    [MapperIgnoreTarget(nameof(AssignmentSolution.File))]
    public partial AssignmentSolution Map(ManageAssignmentSolutionDto dto);
}
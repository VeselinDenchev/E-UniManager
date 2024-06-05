using EUniManager.Application.Models.Assignments.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class AssignmentMapper
{
    public partial List<AssignmentDto> Map(List<Assignment> entities);
    
    public partial AssignmentDto MapAssignmentToAssignmentDto(Assignment entity);
    
    [MapProperty(nameof(Assignment.Type), nameof(AssignmentDetailsDto.Type), Use = nameof(GetTypeString))]
    public partial AssignmentDetailsDto MapAssignmentToAssignmentDetailsDto(Assignment entity);
    
    [MapperIgnoreTarget(nameof(Assignment.Id))]
    [MapperIgnoreTarget(nameof(Assignment.CreatedAt))]
    [MapperIgnoreTarget(nameof(Assignment.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Assignment.Resource))]
    [MapperIgnoreTarget(nameof(Assignment.Students))]
    [MapperIgnoreTarget(nameof(Assignment.Teacher))]
    public partial Assignment Map(CreateAssignmentDto dto);
    
    [MapperIgnoreTarget(nameof(Assignment.Id))]
    [MapperIgnoreTarget(nameof(Assignment.CreatedAt))]
    [MapperIgnoreTarget(nameof(Assignment.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Assignment.Resource))]
    [MapperIgnoreTarget(nameof(Assignment.Students))]
    [MapperIgnoreTarget(nameof(Assignment.Teacher))]
    public partial Assignment Map(UpdateAssignmentDto dto);

    private string GetTypeString(AssignmentType assignmentType)
    {
        return assignmentType switch
        {
            AssignmentType.Text => "Текст",
            AssignmentType.File => "Файл",
            _ => throw new ArgumentException("Such assignment type doesn't exist!")
        };
    }
}
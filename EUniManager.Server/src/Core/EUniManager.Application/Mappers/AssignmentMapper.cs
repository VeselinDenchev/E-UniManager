using EUniManager.Application.Models.Assignments.Dtos;
using EUniManager.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class AssignmentMapper
{
    [MapperIgnoreTarget(nameof(Assignment.Id))]
    [MapperIgnoreTarget(nameof(Assignment.CreatedAt))]
    [MapperIgnoreTarget(nameof(Assignment.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Assignment.Resource))]
    [MapperIgnoreTarget(nameof(Assignment.Students))]
    [MapperIgnoreTarget(nameof(Assignment.Teacher))] // just necessary data
    public partial Assignment Map(CreateAssignmentDto dto);
    
    // [MapperIgnoreTarget(nameof(Assignment.Id))]
    // [MapperIgnoreTarget(nameof(Assignment.CreatedAt))]
    // [MapperIgnoreTarget(nameof(Assignment.ModifiedAt))]
    // [MapperIgnoreTarget(nameof(Assignment.Resource))]
    // [MapperIgnoreTarget(nameof(Assignment.Students))]
    // [MapperIgnoreTarget(nameof(Assignment.Teacher))] // just necessary data
    // public partial Assignment Map(UpdateAssignmentDto dto);
    
    public partial List<AssignmentDto> Map(List<Assignment> entities);
    
    public partial AssignmentDetailsDto Map(Assignment entity);
}
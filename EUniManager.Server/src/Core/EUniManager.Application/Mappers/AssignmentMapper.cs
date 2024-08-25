using EUniManager.Application.Models.Assignments.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

using static EUniManager.Application.Extensions.DateTimeExtensions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class AssignmentMapper
{
    public partial List<AssignmentDto> Map(List<Assignment> entities);
    
    [MapProperty(nameof(Assignment.Type), nameof(AssignmentDetailsDto.Type), Use = nameof(GetTypeString))]
    public partial AssignmentDto MapAssignmentToAssignmentDto(Assignment entity);
    
    [MapProperty(nameof(Assignment.Type), nameof(AssignmentDetailsDto.Type), Use = nameof(GetTypeString))]
    public partial AssignmentDetailsDto MapAssignmentToAssignmentDetailsDto(Assignment entity);
    
    [MapProperty(nameof(Assignment.Type), nameof(AssignmentDetailsDto.Type), Use = nameof(GetTypeString))]
    [MapProperty(nameof(Assignment.StartDate), nameof(AssignmentDetailsDto.StartDate), Use = nameof(FormatDateToBulgarianDateTime))]
    [MapProperty(nameof(Assignment.DueDate), nameof(AssignmentDetailsDto.DueDate), Use = nameof(FormatDateToBulgarianDateTime))]
    public partial AssignmentWithSolutionDto MapAssignmentToAssignmentWithSolutionDto(Assignment entity);
    
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

    private string FormatDateToBulgarianDateTime(DateTime dateTime) => dateTime.ToBulgarianDateTimeFormatString();
}
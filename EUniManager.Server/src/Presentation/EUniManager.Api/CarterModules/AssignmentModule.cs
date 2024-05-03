using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Assignments.Dtos;
using EUniManager.Application.Models.Assignments.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class AssignmentModule()
    : CrudCarterModule<
        IAssignmentService, 
        Assignment, 
        AssignmentDto, 
        AssignmentDetailsDto, 
        CreateAssignmentDto, 
        UpdateAssignmentDto>
        (string.Format(BASE_PATH_TEMPLATE, nameof(Assignment).ToLowerInvariant()));
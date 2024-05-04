using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.AssigmentSolutions.Dtos;
using EUniManager.Application.Models.AssigmentSolutions.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class AssignmentSolutionModule()
    : CrudCarterModule<
        IAssignmentSolutionService,
        AssignmentSolution,
        AssignmentSolutionDto,
        AssignmentSolutionDetailsDto,
        CreateAssignmentSolutionDto,
        UpdateAssignmentSolutionDto>
        (string.Format(BASE_PATH_TEMPLATE, "assignment-solution"));
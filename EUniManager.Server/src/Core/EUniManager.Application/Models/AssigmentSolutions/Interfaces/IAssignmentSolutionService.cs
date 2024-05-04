using EUniManager.Application.Models.AssigmentSolutions.Dtos;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.AssigmentSolutions.Interfaces;

public interface IAssignmentSolutionService :
    IBaseService<AssignmentSolution, Guid, AssignmentSolutionDto, AssignmentSolutionDetailsDto>;
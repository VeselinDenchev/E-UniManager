using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Assignments.Dtos;
using EUniManager.Application.Models.Assignments.Interfaces;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Persistence;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class AssignmentService : BaseService<Assignment, Guid, AssignmentDto, AssignmentDetailsDto>, IAssignmentService
{
    private readonly AssignmentMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public AssignmentService(EUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager) 
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<AssignmentDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Assignment> assignmentEntities = await GetAllEntitiesAsync(cancellationToken);
        List<AssignmentDto> assignmentDtos = _mapper.Map(assignmentEntities);

        return assignmentDtos;
    }

    public override async ValueTask<AssignmentDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Assignment? assignmentEntity = await GetEntityByIdAsync(id, cancellationToken);
        ArgumentNullException.ThrowIfNull(assignmentEntity);

        AssignmentDetailsDto assignmentDto = _mapper.Map(assignmentEntity);

        return assignmentDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        Assignment assignment = _mapper.Map((dto as CreateAssignmentDto)!);
        
        var admin = await _userManager.FindByNameAsync("admin@email.com");
        // find teacher
        //assignment.Teacher = admin;

        await CreateAsync(assignment, cancellationToken);
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        Assignment? assignmentEntity = await GetEntityByIdAsync(id, cancellationToken);
        ArgumentNullException.ThrowIfNull(assignmentEntity);
        
        UpdateAssignmentDto assignmentDto = (UpdateAssignmentDto)dto;

        assignmentEntity.Title = assignmentDto.Title;
        assignmentEntity.StartDate = assignmentDto.StartDate;
        assignmentEntity.DueDate = assignmentDto.DueDate;
        assignmentEntity.Description = assignmentDto.Description;

        await UpdateAsync(assignmentEntity, cancellationToken);
    }
}
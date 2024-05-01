using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Teachers.Dtos;
using EUniManager.Application.Models.Teachers.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Persistence;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class TeacherService : BaseService<Teacher, Guid, TeacherDto, TeacherDetailsDto>, ITeacherService
{
    private readonly TeacherMapper _teacherMapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;

    public TeacherService(EUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
        : base(dbContext)
    {
        _userManager = userManager;
    }


    public override async Task<List<TeacherDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Teacher> teacherEntities = await GetAllEntitiesAsync(cancellationToken);
        List<TeacherDto> teacherDtos = _teacherMapper.Map(teacherEntities);

        return teacherDtos;
    }

    public override async ValueTask<TeacherDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Teacher? teacherEntity = await GetEntityByIdAsync(id, cancellationToken);
        ArgumentNullException.ThrowIfNull(teacherEntity);

        TeacherDetailsDto teacherDto = _teacherMapper.Map(teacherEntity);

        return teacherDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        Teacher teacher = _teacherMapper.Map((dto as CreateTeacherDto)!);
        
        var admin = await _userManager.FindByNameAsync("admin@email.com");
        teacher.User = admin;

        await CreateAsync(teacher, cancellationToken);
    }
    
    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        Teacher teacher = _teacherMapper.Map((dto as UpdateTeacherDto)!);
        teacher.Id = id;
        
        var admin = await _userManager.FindByNameAsync("admin@email.com");
        teacher.User = admin;

        await UpdateAsync(teacher, cancellationToken);
    }

}
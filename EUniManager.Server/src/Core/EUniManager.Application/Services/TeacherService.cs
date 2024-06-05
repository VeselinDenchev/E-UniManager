using EUniManager.Application.Extensions;
using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Teachers.Dtos;
using EUniManager.Application.Models.Teachers.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class TeacherService
    : BaseService<Teacher, Guid, TeacherDto, TeacherDetailsDto>, ITeacherService
{
    private readonly TeacherMapper _teacherMapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;

    public TeacherService(IEUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
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
        Teacher? teacherEntity = await _dbSet.AsNoTracking()
                                             .Include(t => t.User)
                                             .Include(t =>
                                                 t.LecturingSubjects.Where(ls => !ls.Specialty.HasGraduated &&
                                                                                 ls.Semester / ls.Specialty.CurrentYear <= 1))
                                             .ThenInclude(ls => ls.Specialty)
                                             .Include(t =>
                                                 t.LecturingSubjects.Where(ls => !ls.Specialty.HasGraduated &&
                                                                                 ls.Semester / ls.Specialty.CurrentYear <= 1))
                                             .ThenInclude(ls => ls.Course)
                                             .Include(t =>
                                                 t.AssistingSubjects.Where(asub => !asub.Specialty.HasGraduated &&
                                                                                   asub.Semester / asub.Specialty.CurrentYear <= 1))
                                             .ThenInclude(asub => asub.Specialty)
                                             .Include(t =>
                                                 t.AssistingSubjects.Where(asub => !asub.Specialty.HasGraduated &&
                                                                                   asub.Semester / asub.Specialty.CurrentYear <= 1))
                                             .ThenInclude(asub => asub.Course)
                                             .FirstOrDefaultAsync(t => t.Id == id, cancellationToken) ??
                                  throw new ArgumentException($"Such {nameof(Teacher)} doesn't exist!");

        TeacherDetailsDto teacherDto = _teacherMapper.Map(teacherEntity);

        return teacherDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createTeacherDto = (dto as CreateTeacherDto)!;
        Teacher teacher = _teacherMapper.Map(createTeacherDto);
        teacher.User = new IdentityUser<Guid>
        {
            Email = createTeacherDto.Email,
            UserName = createTeacherDto.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        IdentityResult identityResult = await _userManager.CreateAsync(teacher.User, createTeacherDto.Password);
        identityResult.ThrowExceptionIfFailed();

        identityResult = await _userManager.AddToRoleAsync(teacher.User, nameof(UserRole.Teacher));
        identityResult.ThrowExceptionIfFailed();

        await CreateEntityAsync(teacher, cancellationToken);
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        bool exists = await _dbSet.AnyAsync(t => t.Id == id, cancellationToken);
        if (!exists) throw new ArgumentException($"Such {nameof(Teacher).ToLowerInvariant()} doesn't exist!");

        Teacher teacher = _teacherMapper.Map((dto as UpdateTeacherDto)!);
        teacher.Id = id;

        await UpdateEntityAsync(teacher, cancellationToken);
    }

    // Non-deletable
    public override Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Faculties.Dtos;
using EUniManager.Application.Models.Faculties.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class FacultyService(IEUniManagerDbContext dbContext)
    : BaseService<Faculty, Guid, FacultyDto, FacultyDetailsDto>(dbContext), IFacultyService
{
    private readonly FacultyMapper _facultyMapper = new();

    public override async Task<List<FacultyDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Faculty> facultyEntities = await GetAllEntitiesAsync(cancellationToken);
        List<FacultyDto> facultyDtos = _facultyMapper.Map(facultyEntities);

        return facultyDtos;
    }

    public override async ValueTask<FacultyDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Faculty facultyEntity = await _dbSet.Include(f => f.Specialties)
                                            .FirstOrDefaultAsync(f => f.Id == id, cancellationToken) ??
                                throw new ArgumentException($"Such {nameof(Faculty).ToLowerInvariant()} doesn't exist!");

        FacultyDetailsDto assignmentDto = _facultyMapper.Map(facultyEntity);

        return assignmentDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        Faculty faculty = _facultyMapper.Map((dto as ManageFacultyDto)!);
        await CreateEntityAsync(faculty, cancellationToken);
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        bool exists = await ExistsAsync(id, cancellationToken);
        if (!exists) throw new ArgumentException($"Such {nameof(Student).ToLowerInvariant()} doesn't exist");
        
        Faculty faculty = _facultyMapper.Map((dto as ManageFacultyDto)!);
        faculty.Id = id;

        await UpdateEntityAsync(faculty, cancellationToken);
    }

    // Non-deletable
    public override Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Specialties.Dtos;
using EUniManager.Application.Models.Specialties.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class SpecialtyService(IEUniManagerDbContext dbContext)
    : BaseService<Specialty, Guid, SpecialtyDto, SpecialtyDetailsDto>(dbContext), ISpecialtyService
{
    private readonly SpecialtyMapper _specialtyMapper = new();

    public override async Task<List<SpecialtyDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Specialty> specialtyEntities = await _dbSet.Include(s => s.Faculty)
                                                        .ToListAsync(cancellationToken);
        List<SpecialtyDto> specialtyDtos = _specialtyMapper.Map(specialtyEntities);

        return specialtyDtos;
    }

    public override async ValueTask<SpecialtyDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Specialty specialtyEntity = await _dbSet.Include(s => s.Faculty)
                                                .Include(s => s.Students)
                                                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken) ??
                                    throw new ArgumentException($"Such {nameof(Specialty).ToLowerInvariant()} doesn't exist!");;

        SpecialtyDetailsDto specialtyDto = _specialtyMapper.Map(specialtyEntity);

        return specialtyDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createSpecialtyDto = (dto as CreateSpecialtyDto)!;

        // Change with FluentValidator
        if (createSpecialtyDto.FirstAcademicYearStart + createSpecialtyDto.CurrentYear > DateTime.Now.Year)
        {
            throw new ArgumentException("Invalid first academic year or current year!");
        }
        
        Specialty specialty = _specialtyMapper.Map(createSpecialtyDto);
        specialty.Faculty = await _dbContext.Faculties.FindAsync(createSpecialtyDto.FacultyId) ??
                            throw new ArgumentException($"Such {nameof(specialty.Faculty).ToLowerInvariant()} doesn't exist!");
        
        await CreateEntityAsync(specialty, cancellationToken);
    }

    // Non-updatable
    public override Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    // Non-deletable
    public override Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SpecialtyDto>> GetAllByFaculty(Guid facultyId, CancellationToken cancellationToken)
    {
        List<Specialty> specialtyEntities = await _dbSet.Include(s => s.Faculty)
                                                        .Where(s => s.Faculty.Id == facultyId)
                                                        .ToListAsync(cancellationToken);
        
        List<SpecialtyDto> specialtyDtos = _specialtyMapper.Map(specialtyEntities);

        return specialtyDtos;
    }

    public async Task GraduateAsync(Guid id, CancellationToken cancellationToken)
    {
        int rowsAffected = await _dbSet.Where(s => s.Id == id)
                                       .ExecuteUpdateAsync(s => s.SetProperty(sp => sp.HasGraduated, true),
                                                           cancellationToken);

        if (rowsAffected > 0) return;
        
        throw new ArgumentException($"Such {nameof(Specialty).ToLowerInvariant()} doesn't exist!");
    }
    
    public async Task IncrementAcademicYearAsync(CancellationToken cancellationToken)
    {
        await _dbSet.Where(s => !s.HasGraduated)
                    .ExecuteUpdateAsync(s => s.SetProperty(sp => sp.CurrentYear, sp => sp.CurrentYear + 1), 
                                        cancellationToken);
    }
}
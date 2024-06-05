using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Specialties.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Specialties.Interfaces;

public interface ISpecialtyService : IBaseService<Specialty, Guid, SpecialtyDto, SpecialtyDetailsDto>
{
    Task<List<SpecialtyDto>> GetAllByFaculty(Guid facultyId, CancellationToken cancellationToken);
    
    Task GraduateAsync(Guid id, CancellationToken cancellationToken);
    
    Task IncrementAcademicYearAsync(CancellationToken cancellationToken);
}